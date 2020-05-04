using Sirenix.OdinInspector;
using UnityEngine;

namespace IdleTransport.Utilities.DynamicScrollList.Pooling
{
	public abstract class PoolingObject : SerializedMonoBehaviour, IPooling
    {
		public virtual string objectName{ get { return ""; } }
		public bool isUsing { get; set; }

        public virtual void OnCollect()
        {
			isUsing = true;
            gameObject.SetActive(true);
        }

        public virtual void OnRelease()
        {
			isUsing = false;
            gameObject.SetActive(false);
        }
    }
}
