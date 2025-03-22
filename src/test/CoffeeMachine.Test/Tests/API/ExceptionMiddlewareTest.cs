namespace CoffeeMachine.Test.Tests.API
{
    public class ExceptionMiddlewareTest
    {
        [Fact]
        public async Task InvokeAsyncFirstdayofaprilExceptionTest() 
        {
            var context = MockObjectFactory.CreateDefaultHttpContextMock(DateTime.Now.Day, DateTime.Now.Month);
            var requestDelegate = MockObjectFactory.CreateRequestDelegateMock(418);

            var middleWare = new ExceptionMiddleware(requestDelegate);

            await middleWare.InvokeAsync(context);

            Assert.Equal(context.Response?.StatusCode, 418);
        }

        [Fact]
        public async Task InvokeAsyncInternalServerExceptionTest()
        {
            var context = MockObjectFactory.CreateDefaultHttpContextMock(DateTime.Now.Day, DateTime.Now.Month);
            var requestDelegate = MockObjectFactory.CreateRequestDelegateMock(500);

            var middleWare = new ExceptionMiddleware(requestDelegate);

            await middleWare.InvokeAsync(context);

            Assert.Equal(context.Response?.StatusCode, 500);
        }
    }
}
