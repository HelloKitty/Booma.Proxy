using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Booma.Proxy
{
	[TestFixture]
	public static class EventSystemTests
	{
		[Test]
		public static void Test_BaseSingleEventListenerInitializable_Works()
		{
			//arrange
			TestEventInterfaceImpl subscriable = new TestEventInterfaceImpl();
			TestSingleEventChild obj = new TestSingleEventChild(subscriable);

			obj.OnGameInitialized().Wait();

			subscriable.CallEvent();

			//We failt if success isn't thrown first
			Assert.Fail();
		}

		[Test]
		public static void Test_BaseSingleEventListenerInitializable_Works_With_EventHandlerNonGeneric()
		{
			//arrange
			TestEventInterfaceImplNonGeneric subscriable = new TestEventInterfaceImplNonGeneric();
			TestSingleEventChildNonGeneric obj = new TestSingleEventChildNonGeneric(subscriable);

			obj.OnGameInitialized().Wait();

			subscriable.CallEvent();

			//We failt if success isn't thrown first
			Assert.Fail();
		}
	}

	public class TestSingleEventChild : BaseSingleEventListenerInitializable<TestEventInterface, int>
	{
		/// <inheritdoc />
		public TestSingleEventChild(TestEventInterface subscriptionService)
			: base(subscriptionService)
		{

		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, int args)
		{
			Assert.Pass($"Called the {nameof(OnEventFired)} Val: {args}");
		}
	}

	public interface TestEventInterface
	{
		event EventHandler<int> TestEvent;
	}

	public class TestEventInterfaceImpl : TestEventInterface
	{
		/// <inheritdoc />
		public event EventHandler<int> TestEvent;

		public void CallEvent()
		{
			TestEvent?.Invoke(this, 5);
		}
	}

	//The non generic version

	public class TestSingleEventChildNonGeneric : BaseSingleEventListenerInitializable<TestEventInterfaceNonGeneric>
	{
		/// <inheritdoc />
		public TestSingleEventChildNonGeneric(TestEventInterfaceNonGeneric subscriptionService)
			: base(subscriptionService)
		{

		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, EventArgs args)
		{
			Assert.Pass($"Called the {nameof(OnEventFired)} Val: {args}");
		}
	}

	public interface TestEventInterfaceNonGeneric
	{
		event EventHandler TestEvent;
	}

	public class TestEventInterfaceImplNonGeneric : TestEventInterfaceNonGeneric
	{
		/// <inheritdoc />
		public event EventHandler TestEvent;

		public void CallEvent()
		{
			TestEvent?.Invoke(this, EventArgs.Empty);
		}
	}
}
