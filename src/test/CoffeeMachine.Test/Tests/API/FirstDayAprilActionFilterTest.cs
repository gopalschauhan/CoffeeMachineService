namespace CoffeeMachine.Test.Tests.API
{
    public class FirstDayAprilActionFilterTest
    {
        [Fact]
        public void FirstDayAprilActionFilterValidationErrorTest()
        {
            var context = MockObjectFactory.CreateActionExecutingContextMock(DateTime.Now.Day, DateTime.Now.Month);
            var actionDelegate = MockObjectFactory.CreateActionExecutionDelegateMock(context);

            var filter = new FirstDayAprilActionFilter();

            Func<Task> action = async () => await filter.OnActionExecutionAsync(context, actionDelegate);

            var ex = Assert.ThrowsAsync<FirstDayOfAprilException>(action);
           
            Assert.Equal("Status418ImATeapot", ex?.Result.Message);
        }

        [Fact]
        public async Task FirstDayAprilActionFilterSuccessTest()
        {
            var context = MockObjectFactory.CreateActionExecutingContextMock(1, 4);
            var actionDelegate = MockObjectFactory.CreateActionExecutionDelegateMock(context);

            var filter = new FirstDayAprilActionFilter();
            Task filterAction = filter.OnActionExecutionAsync(context, actionDelegate);

            await filterAction;

            Assert.True(filterAction.IsCompletedSuccessfully);
        }
    }
}
