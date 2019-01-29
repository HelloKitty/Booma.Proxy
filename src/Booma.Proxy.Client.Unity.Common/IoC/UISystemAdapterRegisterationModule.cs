using System.Linq;
using Autofac;
using Guardians;
using SceneJect.Common;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

namespace Booma.Proxy
{

	public sealed class UISystemAdapterRegisterationModule : NonBehaviourDependency
	{
		/// <inheritdoc />
		public override void Register(ContainerBuilder register)
		{
			//Because of active load scene, we have to iterate each scene
			for(int i = 0; i < SceneManager.sceneCount; i++)
				foreach(var go in SceneManager.GetSceneAt(i).GetRootGameObjects())
				{
					foreach(var registerable in go.GetComponentsInChildren<SerializedMonoBehaviour>()
						.Select(m => m as IUIAdapterRegisterable)
						.Where(m => m != null))
					{
						//Registers the adapter with the specified Key and Service Type.
						register.RegisterInstance(registerable)
							.SingleInstance()
							.Keyed(registerable.RegisterationKey, registerable.UISerivdeType);
					}
				}
		}
	}
}