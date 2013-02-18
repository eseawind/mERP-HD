using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using Feng.Utils;
using Feng.Net;

namespace Hd.NetRead
{
    /// <summary>
    /// nbeport�ϵ����ݶ�ȡ
    /// </summary>
    public sealed class nbeportRead : WebProxy
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public nbeportRead()
        {
        }

        private string m_url = "http://60.190.1.176/nbedipub/search/new_search/search_list.asp"; //"http://www.nbeport.gov.cn/nbedi/search/new_search/search_list.asp";
        //private string m_loginUrl = "http://www.nbeport.gov.cn/pkmslogin.form";
        //private string m_loginDisplaceUrl = "http://www.nbeport.gov.cn/pkmsdisplace";

        private string m_���ص���תUrl = "http://www.nbeport.gov.cn/nbedi/search/new_search/moni_search.asp";
        private string m_���ص���תData = "moni_entry_id=#���ص����#&button=%BF%AA%CA%BC%B2%E9%D1%AF";
        private Regex m_���ص���תregex = new Regex(@"<div align=""center"">(.*?)</div>", RegexOptions.Singleline);
        private Regex m_HtmlTagRegex = new Regex(@"<.*?>", RegexOptions.Singleline);

        private void Login()
        {
            //string ret = GetPostString(m_loginUrl, "login-form-type=pwd&username=" + m_userName + "&password=" + m_passWord);
            //if (ret.Contains("PKMS Administration: Session Displacement"))
            //{
            //    GetString(m_loginDisplaceUrl);
            //}
        }
        private string m_userName, m_passWord;
        /// <summary>
        /// ���õ�¼���û�������
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        public void SetLoginInfo(string userName, string passWord)
        {
            m_userName = userName;
            m_passWord = passWord;
        }

        private int m_maxResult = 200;
        /// <summary>
        /// MaxResult
        /// </summary>
        public int MaxResult
        {
            get { return m_maxResult; }
            set { m_maxResult = value; }
        }

        private const int m_resultPerPage = 20;

        private bool m_incompleteBoxData;
        /// <summary>
        /// ��װ����Ϣδ��ȡ��ȫ
        /// </summary>
        public bool IncompleteBoxData
        {
            get { return m_incompleteBoxData; }
        }

        #region "Interface"
        /// <summary>
        /// ��ѯ��װ������
        /// </summary>
        /// <param name="�����ڱ�־"></param>
        /// <param name="�ᵥ��"></param>
        /// <returns></returns>
        public IList<��װ������> ��ѯ��װ������(ImportExportType �����ڱ�־, string �ᵥ��)
        {
            return Grab(�����ڱ�־, null, null, null, null, null, null, null, �ᵥ��);
        }

        public IList<��װ������> ��ѯ��װ������(ImportExportType �����ڱ�־, string �ᵥ��, string ����)
        {
            return Grab(�����ڱ�־, null, null, ����, null, null, null, null, �ᵥ��);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="�����ڱ�־"></param>
        /// <param name="���"></param>
        /// <returns></returns>
        public IList<��װ������> ��ѯ��װ������ͨ�����(ImportExportType �����ڱ�־, string ���)
        {
            return Grab(�����ڱ�־, null, null, null, ���, null, null, null, null);
        }

        /// <summary>
        /// ͨ�ò�ѯ����
        /// </summary>
        /// <returns></returns>
        public IList<��װ������> Grab(ImportExportType ����������, string ����, string ����Ӣ������, string ����, string ��װ���,
            DateTime? ����ʱ����, DateTime? ����ʱ��ֹ, string ����UN����, string �ᵥ��)
        {
            Login();

            if (����ʱ����.HasValue)
            {
                ����ʱ���� = DateTimeHelper.GetDateTimeStartofDay(����ʱ����.Value);
            }
            if (����ʱ��ֹ.HasValue)
            {
                ����ʱ��ֹ = DateTimeHelper.GetDateTimeEndofDay(����ʱ��ֹ.Value);
            }

            List<��װ������> totalBoxData = new List<��װ������>();
            List<string> boxStringData = new List<string>();

            string pageSource, boxSource;

            // ��һҳ
            string firstPostData = CreatePostData(����������, ����, ����Ӣ������, ����, ��װ���, ����ʱ����, ����ʱ��ֹ, ����UN����, �ᵥ��);
            string webContent = PostToString(m_url, firstPostData);

            nbeport.PageAnalyze.SplitWebContent(webContent, out pageSource, out boxSource);

            if (pageSource == null || boxSource == null)
            {
                return totalBoxData;
            }

            int totalPages = nbeport.PageAnalyze.GetPageCount(pageSource);
            int totalBoxs = nbeport.PageAnalyze.GetBoxCount(pageSource);
            boxStringData.AddRange(nbeport.PageAnalyze.GetBoxData(boxSource));

            int needPage = (m_maxResult - 1) / m_resultPerPage + 1;
            if (totalPages > needPage)
            {
                m_incompleteBoxData = true;
                totalPages = needPage;
            }

            for (int page = 1; page < totalPages; page++)
            {
                System.Threading.Thread.Sleep(1000);

                string postData = CreateGotoPostData(page, firstPostData);

                webContent = PostToString(m_url, postData);

                nbeport.PageAnalyze.SplitWebContent(webContent, out pageSource, out boxSource);

                boxStringData.AddRange(nbeport.PageAnalyze.GetBoxData(boxSource));
            }

            if (boxStringData.Count % 9 != 0)
            {
                throw new WebFormatChangedException("invalid box data count");
            }

            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("zh-CN");
            for (int i = 0; i < boxStringData.Count / 9; ++i)
            {
                DateTime jcsj = DateTime.ParseExact(boxStringData[9 * i + 5], "yyyy��MM��dd��HH��mm��ss��", culture);

                if (!string.IsNullOrEmpty(����) && ���� != boxStringData[9 * i].Trim())
                    continue;
                if (!string.IsNullOrEmpty(����Ӣ������) && ����Ӣ������ != boxStringData[9 * i + 1].Trim())
                    continue;
                if (!string.IsNullOrEmpty(����) && ���� != boxStringData[9 * i + 2].Trim())
                    continue;
                if (!string.IsNullOrEmpty(��װ���) && ��װ��� != boxStringData[9 * i + 3].Trim())
                    continue;
                if (!string.IsNullOrEmpty(����UN����) && ����UN���� != boxStringData[9 * i + 4].Trim())
                    continue;
                if (����ʱ���� != null && jcsj < ����ʱ����.Value)
                    continue;
                if (����ʱ��ֹ != null && jcsj > ����ʱ��ֹ.Value)
                    continue;
                if (!string.IsNullOrEmpty(�ᵥ��) && �ᵥ�� != boxStringData[9 * i + 7].Trim())
                    continue;

                totalBoxData.Add(new ��װ������(boxStringData[9 * i].Trim(),
                    boxStringData[9 * i + 1].Trim(),
                    boxStringData[9 * i + 2].Trim(),
                    boxStringData[9 * i + 3].Trim(),
                    boxStringData[9 * i + 4].Trim(),
                    jcsj,
                    null,
                    boxStringData[9 * i + 6].Trim(),
                    boxStringData[9 * i + 7].Trim(),
                    boxStringData[9 * i + 8].Trim(),
                    null));
            }
            return totalBoxData;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="�����ڱ�־"></param>
        ///// <param name="tdh"></param>
        ///// <returns></returns>
        //private string GetBoxUrlByTdh(ImportExportType �����ڱ�־, string tdh)
        //{
        //    return m_url + "?" + CreatePostData(�����ڱ�־, null, null, null, null, null, null, null, tdh);
        //}


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="�����ڱ�־"></param>
        ///// <param name="xh"></param>
        ///// <returns></returns>
        //private string GetBoxUrlByXh(ImportExportType �����ڱ�־, string xh)
        //{
        //    return m_url + "?" + CreatePostData(�����ڱ�־, null, null, null, xh, null, null, null, null);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="���ص����"></param>
        /// <returns></returns>
        public IList<��ת״̬����> ��ѯ��ת״̬����(string ���ص����)
        {
            IList<��ת״̬����> ret = new List<��ת״̬����>();

            try
            {
                string postData = m_���ص���תData.Replace("#���ص����#", ���ص����);
                string htmlInfo = base.PostToString(m_���ص���תUrl, postData);
                if (htmlInfo.Contains("/portalframework/um/Login.do"))
                {
                    Login();
                    htmlInfo = base.PostToString(m_���ص���תUrl, postData);
                }
                MatchCollection mc = m_���ص���תregex.Matches(htmlInfo);
                if (mc.Count % 5 != 0)
                {
                    throw new WebFormatChangedException("nbeport html format is changed");
                }

                for (int i = 1; i < mc.Count / 5; ++i)
                {
                    ret.Add(new ��ת״̬����(WebProxy.RemoveSpaces(mc[i * 5 + 2].Groups[1].Value),
                        Convert.ToDateTime(WebProxy.RemoveSpaces(mc[i * 5 + 4].Groups[1].Value))));
                    //s = base.RemoveSpaces(s);
                }
            }
            catch (Exception)
            {
                //ServiceProvider.GetService<IExceptionProcess>().ProcessWithNotify(ex);
            }
            return ret;
        }

        #endregion



        /// <summary>
        /// ���ɳ��ν��뽻����������
        /// </summary>
        /// <returns></returns>
        private string CreatePostData(ImportExportType ����������, string ����, string ����Ӣ������, string ����, string ��װ���,
            DateTime? ����ʱ����, DateTime? ����ʱ��ֹ, string ����UN����, string �ᵥ��)
        {
            StringBuilder postData = new StringBuilder();

            if (!string.IsNullOrEmpty(�ᵥ��))
            {
                postData.Append("BILL_NO=" + �ᵥ��);
            }

            if (!string.IsNullOrEmpty(����))
            {
                postData.Append("&SHIP_NAME=" + ����);
            }

            if (!string.IsNullOrEmpty(����Ӣ������))
            {
                postData.Append("&SHIP_NAME_EN=" + ����Ӣ������);
            }

            if (!string.IsNullOrEmpty(����))
            {
                postData.Append("&FLIGHT_NO=" + ����);
            }

            if (!string.IsNullOrEmpty(��װ���))
            {
                postData.Append("&JZX_NO=" + ��װ���);
            }

            if (����ʱ����.HasValue)
            {
                postData.Append("&JC_DATE_B=" + ����ʱ����.Value.ToString("yyyy-MM-dd"));
            }

            if (����ʱ��ֹ.HasValue)
            {
                postData.Append("&JC_DATE_E=" + ����ʱ��ֹ.Value.ToString("yyyy-MM-dd"));
            }

            if (!string.IsNullOrEmpty(����UN����))
            {
                postData.Append("&SHIP_UN_NO=" + ����UN����);
            }

            postData.Append("&button=%BF%AA%CA%BC%B2%E9%D1%AF");

            switch (����������)
            {
                case ImportExportType.���ڼ�װ��:
                    postData.Append("&IE_FLAG=" + "E");
                    break;
                case ImportExportType.���ڼ�װ��:
                    postData.Append("&IE_FLAG=" + "I");
                    break;
                default:
                    throw new NotSupportedException("Invalid ImportExport Type");
            }

            string s = postData.ToString();
            if (s[0] == '&')
            {
                s = s.Remove(0, 1);
            }
            return s;
        }

        /// <summary>
        /// ����վ��������ʵ�ַ�ҳ����
        /// </summary>
        /// <param name="intcur">��ǰҳ��</param>
        /// <param name="postdata">������ԭ����</param>
        /// <returns>���ط�ҳ���ܽ�������</returns>
        private string CreateGotoPostData(int intcur, string postdata)
        {
            string GotoPostData = postdata;
            int intCur = intcur;
            int nextCur = intcur + 1;
            GotoPostData += "&intCur=" + intCur + "&goto=" + nextCur + "&sbtpn=GO";
            return GotoPostData;
        }
    }
}
