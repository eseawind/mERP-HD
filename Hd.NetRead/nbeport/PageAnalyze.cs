using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Feng.Net;

namespace Hd.NetRead.nbeport
{
	internal class PageAnalyze
	{
        private PageAnalyze()
		{
		}

		/// <summary>
		/// ��λ��ʵ��Ҫ���ҵı��
		/// </summary>
        /// <param name="webContent">��ҳԴ��</param>
        /// <param name="pageSource">���ȡ����ҳ������ҳԴ��</param>
        /// <param name="boxSource">��ҳ��װ�����ݱ��Դ��</param>
		/// <returns></returns>
		internal static void SplitWebContent(string webContent, out string pageSource, out string boxSource)
		{
			webContent = webContent.Trim();
			if (string.IsNullOrEmpty(webContent))
			{
				throw new ArgumentNullException("webContent");
			}

			const string boxSourceStart = "<input type=hidden name=intCur value=\'";
			int bIndex = webContent.IndexOf(boxSourceStart);

			const string pageSourceStart = "<tr><td bgcolor=#ffffff colspan=10><div align=center><font class='three'>";
			int eIndex = webContent.IndexOf(pageSourceStart);

			if (bIndex > 0 && eIndex > 0)
			{
				boxSource = webContent.Substring(bIndex, eIndex - bIndex);
				pageSource = webContent.Substring(eIndex);
			}
			else
			{
				boxSource = null;
				pageSource = null;
			}
		}


		private static Regex m_regexPage = new Regex("ҳ����([0-9]+)/([0-9]+)");
		/// <summary>
		/// �õ���ҳ��ҳ��
		/// </summary>
		/// <param name="pageSource"></param>
		/// <returns></returns>
		internal static int GetPageCount(string pageSource)
		{
			MatchCollection mc = m_regexPage.Matches(pageSource);
			if (mc.Count == 1)
			{
				return Convert.ToInt32(mc[0].Groups[2].Value);
			}
			else
			{
				throw new WebFormatChangedException("PageSource format for PageCount is changed");
			}
		}


		private static Regex m_regexTotalBox = new Regex("��([0-9]+)��");
		/// <summary>
		/// �õ���ҳ������Ϣ����
		/// </summary>
		/// <param name="pageSource"></param>
		/// <returns></returns>
		internal static int GetBoxCount(string pageSource)
		{
			MatchCollection mx = m_regexTotalBox.Matches(pageSource);
			if (mx.Count == 1)
			{
				return Convert.ToInt32(mx[0].Groups[1].Value);
			}
			else
			{
				throw new WebFormatChangedException("PageSource format for BoxCount is changed");
			}
		}


        private static Regex m_regexBoxData = new Regex(@"<td[^>]*?>(.*?)</td>");
		private static Regex m_regexBoxInnerData = new Regex(@"<div[^>]*?>(.*?)</div>");
		/// <summary>
		/// ƥ��õ���ҳ�������
		/// </summary>
		/// <param name="boxSource"></param>
		/// <returns></returns>
		internal static IList<string> GetBoxData(string boxSource)
		{
			List<string> ret = new List<string>();
			MatchCollection myMc = m_regexBoxData.Matches(boxSource);
			foreach (Match m in myMc)
			{
				string box = m.Groups[1].Value;
				MatchCollection mc = m_regexBoxInnerData.Matches(box);

				string s;
				if (mc.Count > 0)
				{
					if (mc.Count == 1)
					{
						s = mc[0].Groups[1].Value.Trim();
					}
					else
					{
						throw new WebFormatChangedException("boxSource format is changed");
					}
				}
				else
				{
					s = box.Trim();
				}
				const string nullSpace = "&nbsp;";
				if (s.Contains(nullSpace))
				{
					s = s.Replace(nullSpace, "");
				}
				ret.Add(s);
			}
			return ret;
		}
	}
}
