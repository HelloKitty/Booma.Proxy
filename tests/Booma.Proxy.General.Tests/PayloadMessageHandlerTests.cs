using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Booma.Proxy;
using Fasterflect;
using NUnit.Framework;
using SceneJect.Common;

namespace Booma
{
	[TestFixture]
	public sealed class PayloadMessageHandlerTests
	{
		public static IEnumerable<Type> HandlerTypes 
			=> new ClientUnitySharedHandlersMetadataMarker().AssemblyDefinedHandlerTyped
		.Concat(new ClientUnityAuthenticationMetadataMarker().AssemblyDefinedHandlerTyped)
		.Concat(new ClientUnityCharacterMetadataMarker().AssemblyDefinedHandlerTyped)
		.Concat(new ClientUnityShipMetadataMarker().AssemblyDefinedHandlerTyped)
		.ToArray();

		//This test exists to make sure and validate we aren't expecting and old fields
		//or props to be field/prop injected by sceneject since we are no longer
		//actually in the scene.
		[Test]
		[TestCaseSource(nameof(HandlerTypes))]
		public static void Test_Handler_Contains_No_Scenejected_FieldsOrProps(Type handlerType)
		{
			//arrange
			MemberInfo[] members = handlerType.Members(MemberTypes.All).ToArray();

			//assert
			foreach(var mi in members)
			{
				if(mi.GetCustomAttribute<SceneJect.Common.InjectAttribute>() != null)
				{
					//We have an inject attribute still, we should warn
					Assert.Fail($"Failed. Type: {handlerType} has {nameof(InjectAttribute)} on Field/Prop: {mi.Name}. Handlers no longer scene objects. Must address this.");
				}
			}
		}

		[Test]
		[TestCaseSource(nameof(HandlerTypes))]
		public static void Test_Handler_Contains_No_SerializedField_Members(Type handlerType)
		{
			//arrange
			MemberInfo[] members = handlerType.Members(MemberTypes.All).ToArray();

			//assert
			foreach(var mi in members)
			{
				if(mi.GetCustomAttribute<UnityEngine.SerializeField>() != null)
				{
					//We have an inject attribute still, we should warn
					Assert.Fail($"Failed. Type: {handlerType} has {nameof(UnityEngine.SerializeField)} on Field/Prop: {mi.Name}. Handlers no longer scene objects. Must address this.");
				}
			}
		}
	}
}
