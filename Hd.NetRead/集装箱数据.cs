using System;
using System.Collections.Generic;
using System.Text;

namespace Hd.NetRead
{
    /// <summary>
    /// 
    /// </summary>
	public enum ImportExportType
	{
        /// <summary>
        /// 
        /// </summary>
		���ڼ�װ�� = 1,
        /// <summary>
        /// 
        /// </summary>
		���ڼ�װ�� = 0,
	}

	/// <summary>
	/// ��װ������
	/// ���� ����Ӣ������ ���� ��װ��� ����UN���� ����ʱ�� �ѳ�λ�� �ᵥ�� �ѳ��� 
	///	CSCCQ XIN CHONG QING  0103E CCLU3009908  UN9262118 2007��11��25�� 03��01��33�� L10732  SHHNBMTR3AH881   �������� 
	/// </summary>
	public class ��װ������
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="����"></param>
		/// <param name="����Ӣ������"></param>
		/// <param name="����"></param>
		/// <param name="��װ���"></param>
		/// <param name="����UN����"></param>
		/// <param name="����ʱ��"></param>
		/// <param name="�ѳ�λ��"></param>
		/// <param name="�ᵥ��"></param>
		/// <param name="�ѳ���"></param>
		/// <param name="����"></param>
		public ��װ������(string ����, string ����Ӣ������, string ����, string ��װ���, string ����UN����,
			DateTime ����ʱ��, DateTime? ����ʱ��, string �ѳ�λ��, string �ᵥ��, string �ѳ���, string ����)
		{
			m_���� = ����;
			m_����Ӣ������ = ����Ӣ������;
			m_���� = ����;
			m_��װ��� = ��װ���;
			m_����UN���� = ����UN����;
			m_Real����ʱ�� = ����ʱ��;
            m_Real����ʱ�� = ����ʱ��;
			m_�ѳ�λ�� = �ѳ�λ��;
			m_�ᵥ�� = �ᵥ��;
			m_�ѳ��� = �ѳ���;
			m_���� = ����;
		}

		private string m_����;
		/// <summary>
		/// ����
		/// </summary>
		public string ����
		{
			get { return m_����; }
		}

		private string m_����Ӣ������;
		/// <summary>
		/// ����Ӣ������
		/// </summary>
		public string ����Ӣ������
		{
			get { return m_����Ӣ������; }
		}

		private string m_����;
		/// <summary>
		/// ������
		/// </summary>
		///
		public string ����
		{
			get { return m_����; }
		}

		private string m_��װ���;
		/// <summary>
		/// ����װ���
		/// </summary>
		///
		public string ��װ���
		{
			get { return m_��װ���; }
		}

		private string m_����UN����;
		/// <summary>
		/// ������UN����
		/// </summary>
		///
		public string ����UN����
		{
			get { return m_����UN����; }
		}

		/// <summary>
		/// ʵ�ʽ���ʱ���������
		/// </summary>
		///
		public DateTime ����ʱ��
		{
			get { return new DateTime(m_Real����ʱ��.Year, m_Real����ʱ��.Month, m_Real����ʱ��.Day); }
		}

        public DateTime? ����ʱ��
        {
            get { return new DateTime(m_Real����ʱ��.Value.Year, m_Real����ʱ��.Value.Month, m_Real����ʱ��.Value.Day); }
        }

		private DateTime m_Real����ʱ��;
		/// <summary>
		/// ����ʱ��
		/// �����ʽ��yyyy-MM-dd�� �����ʽ��2007��11��25�� 03��01��33�룩
		/// </summary>
		///
		public DateTime Real����ʱ��
		{
			get { return m_Real����ʱ��; }
		}

        private DateTime? m_Real����ʱ��;

        public DateTime? Real����ʱ��
        {
            get { return m_Real����ʱ��; }
            set { m_Real����ʱ�� = value; }
        }

		private string m_�ѳ�λ��;
		/// <summary>
		/// �ѳ�λ��
		/// </summary>
		public string �ѳ�λ��
		{
			get { return m_�ѳ�λ��; }
		}

		private string m_�ᵥ��;
		/// <summary>
		/// �ᵥ��
		/// </summary>
		public string �ᵥ��
		{
			get { return m_�ᵥ��; }
		}

		private string m_�ѳ���;
		/// <summary>
		/// �ѳ���
		/// </summary>
		public string �ѳ���
		{
			get { return m_�ѳ���; }
            set { m_�ѳ��� = value; }
		}

		private string m_����;
		/// <summary>
		/// ����
		/// </summary>
		public string ����
		{
			get { return m_����; }
            set { m_���� = value; }
		}
	}
}
