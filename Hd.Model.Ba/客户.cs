using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using NHibernate.Mapping.Attributes;
using Feng;
using Feng.Data;

namespace Hd.Model.Ba
{
	public enum �������
	{
		�ܺ� = 1,
		���� = 2,
		һ�� = 3,
		�� = 4,
		���� = 5
	}

	public enum �Ա�
	{
		�� = 1,
		Ů = 2
	}

    [Serializable]
    [Auditable]
    [JoinedSubclass(NameType = typeof(�ͻ�), ExtendsType = typeof(��Ա), Table = "��������_��Ա��λ_�ͻ�")]
    [Key(Column = "���", ForeignKey = "FK_��Ա_�ͻ�")]
    [Cache(Usage = CacheUsage.NonStrictReadWrite)]
    public class �ͻ� : ��Ա, IOperatingEntity
    {
        void IOperatingEntity.PreparingOperate(OperateArgs e)
        {
            if (e.OperateType == OperateType.Save)
            {
                if (string.IsNullOrEmpty(this.���))
                {
                    // Todo
                    int delta = Feng.Utils.RepositoryHelper.GetRepositoryDelta(e.Repository, typeof(�ͻ�).Name);

                    this.��� = PrimaryMaxIdGenerator.GetMaxId("��������_��Ա��λ", "���", 6, "90", delta).ToString();
                }
            }
        }
        void IOperatingEntity.PreparedOperate(OperateArgs e)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public �ͻ�()
        {
            ������� = �������.һ��;
        }

		///<summary>
		///�״ν���
		///</summary>
        [Property(NotNull = false)]
        public virtual DateTime? �״ν���
        {
            get;
            set;
        }

		///<summary>
		///�������
		///</summary>
        [Property(Column = "�������", NotNull = true)]
		public virtual ������� �������
		{
            get;
            set;
		}

		///<summary>
		///���
		///</summary>
		[Property(Length = 200)]
		public virtual string ���
		{
            get;
            set;
		}

		///<summary>
		///������¼
		///</summary>
		[Property(Length = 200)]
		public virtual string ������¼
		{
            get;
            set;
		}
    }
}
