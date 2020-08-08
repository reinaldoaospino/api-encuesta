using Moq;
using Xunit;
using System;
using System.IO;
using api_encuesta.Middlewares;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Api_encuesta.test
{
    public class ExceptionHandlerMiddlewareTest
    {
        private readonly Mock<RequestDelegate> _next;
        private readonly ExceptionHandlerMiddleware _exceptionMiddleware;

        public ExceptionHandlerMiddlewareTest()
        {
            _next = new Mock<RequestDelegate>();
            _exceptionMiddleware = new ExceptionHandlerMiddleware(_next.Object);
        }

        [Fact]
        public async void GivenValidHttpContext_WhenInvoke_ThenInvokeSuccessful()
        {
            //?Given
            var context = new DefaultHttpContext();

            //?When
            await _exceptionMiddleware.Invoke(context);

            //?Then
            _next.Verify(n => n.Invoke(context), Times.Once);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public async void GivenException_WhenInvoke_ThenHandleTheException(Exception exception, int statusCodeExpected)
        {
            //?Given
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            _next.Setup(n => n.Invoke(context))
                .Throws(exception)
                .Verifiable();

            //?When
            await _exceptionMiddleware.Invoke(context);

            //?Then
            Assert.Equal(context.Response.StatusCode, statusCodeExpected);
            _next.Verify();
        }

        public static IEnumerable<object[]> Data =>
      new List<object[]>
      {
                new object[] {new ApplicationException(), StatusCodes.Status400BadRequest},
                new object[] {new Exception(), StatusCodes.Status500InternalServerError}
      };
    }
}
