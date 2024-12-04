namespace InterceptorHelper
{
    using System;
    using System.Threading.Tasks;
    using Grpc.Core;
    using Grpc.Core.Interceptors;

    public class ErrorHandlingInterceptor : Interceptor
    {
        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            try
            {
                // Proceed with the call
                var call = continuation(request, context);

                // Handle the response
                var response = call.ResponseAsync.ContinueWith(task =>
                {
                    if (task.Exception != null)
                    {
                        Console.WriteLine($"Error: {task.Exception.Message}");
                            throw new RpcException(new Status(StatusCode.Internal, " Something went wrong."));
                    }
                    return task.Result;
                });

                return new AsyncUnaryCall<TResponse>(
                    response,
                    call.ResponseHeadersAsync,
                    call.GetStatus,
                    call.GetTrailers,
                    call.Dispose);
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"RPC Error: {ex.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Something went wrong."));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "An unexpected error occurred."));
            }
        }
    }

}
