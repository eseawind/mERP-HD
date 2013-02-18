using System;
using System.Collections.Generic;
using System.Text;

namespace Hd.NetRead
{
	/// <summary>
	/// ���ص�����
    /// ���ص��� ���ص����� �������� ��� �걨���� ��Ӫ��λ ���˵��� ��׼�ĺ� ҳ��
	/// </summary>
	public class ���ص�����
	{
		/// <summary>
		/// Constructor
		/// </summary>
        /// <param name="���ص���"></param>
        /// <param name="���ص�����"></param>
        /// <param name="��������"></param>
        /// <param name="���"></param>
        /// <param name="�걨����"></param>
        /// <param name="��Ӫ��λ"></param>
        /// <param name="���˵���"></param>
        /// <param name="��׼�ĺ�"></param>
        /// <param name="������"></param>
        /// <param name="����"></param>
        /// <param name="��ҳ����"></param>
        /// <param name="ҳ��"></param>
        /// <param name="ͨ�ص���"></param>
        public ���ص�����(string ���ص���, string ���ص�����, string ��������, string ���, DateTime? �걨����, string ��Ӫ��λ,
            string ���˵���, string ��׼�ĺ�, int ����, int ������, string ��ҳ����, int ҳ��, string ͨ�ص���, string ����Ա, string ���ع�˾)
		{
            m_���ص��� = ���ص���;
            m_���ص����� = ���ص�����;
            m_�������� = ��������;
            m_��� = ���;
            m_�걨���� = �걨����;
            m_��Ӫ��λ = ��Ӫ��λ;
            m_���˵��� = ���˵���;
            m_��׼�ĺ� = ��׼�ĺ�;
            m_���� = ����;
            m_������ = ������;
            m_��ҳ���� = ��ҳ����;
            m_ҳ�� = ҳ��;
            m_ͨ�ص��� = ͨ�ص���;
            m_����Ա = ����Ա;
            m_���ع�˾ = ���ع�˾;
		}

		private string m_���ص���;
		/// <summary>
        /// ���ص���
		/// </summary>
        public string ���ص���
		{
            get { return m_���ص���; }
		}

        private string m_���ص�����;
		/// <summary>
        /// ���ص�����
		/// </summary>
        public string ���ص�����
		{
            get { return m_���ص�����; }
		}

		private string m_��������;
		/// <summary>
        /// ����������
		/// </summary>
        public string ��������
		{
            get { return m_��������; }
		}

		private string m_���;
		/// <summary>
		/// �����
		/// </summary>
		///
		public string ���
		{
			get { return m_���; }
		}

		private string m_��Ӫ��λ;
		/// <summary>
        /// ����Ӫ��λ
		/// </summary>
		///
        public string ��Ӫ��λ
		{
            get { return m_��Ӫ��λ; }
		}

        private DateTime? m_�걨����;
		/// <summary>
		/// �걨����
		/// </summary>
		///
        public DateTime? �걨����
		{
            get { return m_�걨����; }
		}

		private string m_���˵���;
		/// <summary>
        /// ���˵���
		/// </summary>
        public string ���˵���
		{
            get { return m_���˵���; }
		}

		private string m_��׼�ĺ�;
		/// <summary>
        /// ��׼�ĺ�
		/// </summary>
        public string ��׼�ĺ�
		{
            get { return m_��׼�ĺ�; }
		}

        private int m_������;
        /// <summary>
        /// ������
        /// </summary>
        public int ������
        {
            get { return m_������; }
        }

        private int m_����;
        /// <summary>
        /// ����
        /// </summary>
        public int ����
        {
            get { return m_����; }
        }

		private string m_��ҳ����;
		/// <summary>
        /// ��ҳ����
		/// </summary>
        public string ��ҳ����
		{
            get { return m_��ҳ����; }
            set { m_��ҳ���� = value; }
		}

        private int m_ҳ��;
        /// <summary>
        /// ҳ��
        /// </summary>
        public int ҳ��
        {
            get { return m_ҳ��; }
            set { m_ҳ�� = value; }
        }

        private string m_ͨ�ص���;
        /// <summary>
        /// ͨ�ص���
        /// </summary>
        public string ͨ�ص���
        {
            get { return m_ͨ�ص���; }
        }

        private string m_����Ա;
        /// <summary>
        /// ����Ա
        /// </summary>
        public string ����Ա
        {
            get { return m_����Ա; }
        }

        private string m_���ع�˾;
        /// <summary>
        /// ���ع�˾
        /// </summary>
        public string ���ع�˾
        {
            get { return m_���ع�˾; }
        }
	}
}
