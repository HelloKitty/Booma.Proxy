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

		[Test]
		public static void Test_BaseSingleEventListenerInitializable_WhenUnsubscriberd_Doesnt_Call_Event_NonGeneric()
		{
			//arrange
			TestEventInterfaceImplNonGeneric subscriable = new TestEventInterfaceImplNonGeneric();
			TestSingleEventChildNonGeneric obj = new TestSingleEventChildNonGeneric(subscriable, false);

			//sub and then unsub.
			obj.OnGameInitialized().Wait();
			obj.UnSub();

			subscriable.CallEvent();

			//We failt if success isn't thrown first
			Assert.Pass($"Succeded, did not call event.");
		}

		[Test]
		public static void Test_BaseSingleEventListenerInitializable_WhenUnsubscriberd_Doesnt_Call_Event()
		{
			//arrange
			TestEventInterfaceImpl subscriable = new TestEventInterfaceImpl();
			TestSingleEventChild obj = new TestSingleEventChild(subscriable, false);

			//sub and then unsub.
			obj.OnGameInitialized().Wait();
			obj.UnSub();

			subscriable.CallEvent();

			//We failt if success isn't thrown first
			Assert.Pass($"Succeded, did not call event.");
		}
	}

	public class TestSingleEventChild : BaseSingleEventListenerInitializable<TestEventInterface, EventArgsTest>
	{
		private bool assertedTrueValue { get; }

		/// <inheritdoc />
		public TestSingleEventChild(TestEventInterface subscriptionService, bool assertedTrueValue = true)
			: base(subscriptionService)
		{
			this.assertedTrueValue = assertedTrueValue;
		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, EventArgsTest args)
		{
			Assert.True(assertedTrueValue, $"Failed to assert the asserted true.");

			Assert.Pass($"Called the {nameof(OnEventFired)} Val: {args.Value}");
		}


		public void UnSub()
		{
			this.Unsubscribe();
		}
	}

	public interface TestEventInterface
	{
		event EventHandler<EventArgsTest> TestEvent;
	}

	public class TestEventInterfaceImpl : TestEventInterface
	{
		/// <inheritdoc />
		public event EventHandler<EventArgsTest> TestEvent;

		public void CallEvent()
		{
			TestEvent?.Invoke(this, new EventArgsTest(5));
		}
	}

	//The non generic version

	public class TestSingleEventChildNonGeneric : BaseSingleEventListenerInitializable<TestEventInterfaceNonGeneric>
	{
		private bool assertedTrueValue { get; }

		/// <inheritdoc />
		public TestSingleEventChildNonGeneric(TestEventInterfaceNonGeneric subscriptionService, bool assertedTrueValue = true)
			: base(subscriptionService)
		{
			this.assertedTrueValue = assertedTrueValue;
		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, EventArgs args)
		{
			Assert.True(assertedTrueValue, $"Failed to assert the asserted true.");

			Assert.Pass($"Called the {nameof(OnEventFired)} Val: {args}");
		}

		public void UnSub()
		{
			this.Unsubscribe();
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

	public class EventArgsTest : EventArgs
	{
		public int Value { get; }

		/// <inheritdoc />
		public EventArgsTest(int value)
		{
			Value = value;
		}
	}
}
