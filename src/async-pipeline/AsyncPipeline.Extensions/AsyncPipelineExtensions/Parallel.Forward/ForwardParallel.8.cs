using System;
using System.Threading;
using System.Threading.Tasks;

namespace GGroupp;

partial class AsyncPipelineExtensions
{
    public static AsyncPipeline<TSuccess, TFailure> ForwardParallel<TIn, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut7, TOut8, TSuccess, TFailure>(
        this AsyncPipeline<TIn, TFailure> pipeline,
        Func<TIn, CancellationToken, Task<Result<TOut1, TFailure>>> firstPipeAsync,
        Func<TIn, CancellationToken, Task<Result<TOut2, TFailure>>> secondPipeAsync,
        Func<TIn, CancellationToken, Task<Result<TOut3, TFailure>>> thirdPipeAsync,
        Func<TIn, CancellationToken, Task<Result<TOut4, TFailure>>> fourthPipeAsync,
        Func<TIn, CancellationToken, Task<Result<TOut5, TFailure>>> fifthPipeAsync,
        Func<TIn, CancellationToken, Task<Result<TOut6, TFailure>>> sixthPipeAsync,
        Func<TIn, CancellationToken, Task<Result<TOut7, TFailure>>> seventhPipeAsync,
        Func<TIn, CancellationToken, Task<Result<TOut8, TFailure>>> eigthPipeAsync,
        Func<TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut7, TOut8, TSuccess> fold)
        where TFailure : struct
    {
        ArgumentNullException.ThrowIfNull(firstPipeAsync);
        ArgumentNullException.ThrowIfNull(secondPipeAsync);
        ArgumentNullException.ThrowIfNull(thirdPipeAsync);
        ArgumentNullException.ThrowIfNull(fourthPipeAsync);
        ArgumentNullException.ThrowIfNull(fifthPipeAsync);
        ArgumentNullException.ThrowIfNull(sixthPipeAsync);
        ArgumentNullException.ThrowIfNull(seventhPipeAsync);
        ArgumentNullException.ThrowIfNull(eigthPipeAsync);
        ArgumentNullException.ThrowIfNull(fold);

        return pipeline.Forward(InnerPipeAsync);

        async Task<Result<TSuccess, TFailure>> InnerPipeAsync(TIn input, CancellationToken cancellationToken)
        {
            var firstTask = firstPipeAsync.Invoke(input, cancellationToken);
            var secondTask = secondPipeAsync.Invoke(input, cancellationToken);
            var thirdTask = thirdPipeAsync.Invoke(input, cancellationToken);
            var fourthTask = fourthPipeAsync.Invoke(input, cancellationToken);
            var fifthTask = fifthPipeAsync.Invoke(input, cancellationToken);
            var sixthTask = sixthPipeAsync.Invoke(input, cancellationToken);
            var seventhTask = seventhPipeAsync.Invoke(input, cancellationToken);
            var eigthTask = eigthPipeAsync.Invoke(input, cancellationToken);

            await Task.WhenAll(firstTask, secondTask, thirdTask, fourthTask, fifthTask, sixthTask, seventhTask, eigthTask).ConfigureAwait(false);

            var firstResult = firstTask.Result;
            if (firstResult.IsFailure)
            {
                return firstResult.FailureOrThrow();
            }

            var secondResult = secondTask.Result;
            if (secondResult.IsFailure)
            {
                return secondResult.FailureOrThrow();
            }

            var thirdResult = thirdTask.Result;
            if (thirdResult.IsFailure)
            {
                return thirdResult.FailureOrThrow();
            }

            var fourthResult = fourthTask.Result;
            if (fourthResult.IsFailure)
            {
                return fourthResult.FailureOrThrow();
            }

            var fifthResult = fifthTask.Result;
            if (fifthResult.IsFailure)
            {
                return fifthResult.FailureOrThrow();
            }

            var sixthResult = sixthTask.Result;
            if (sixthResult.IsFailure)
            {
                return sixthResult.FailureOrThrow();
            }

            var seventhResult = seventhTask.Result;
            if (seventhResult.IsFailure)
            {
                return seventhResult.FailureOrThrow();
            }

            var eigthResult = eigthTask.Result;
            if (eigthResult.IsFailure)
            {
                return eigthResult.FailureOrThrow();
            }

            return fold.Invoke(
                firstResult.SuccessOrThrow(),
                secondResult.SuccessOrThrow(),
                thirdResult.SuccessOrThrow(),
                fourthResult.SuccessOrThrow(),
                fifthResult.SuccessOrThrow(),
                sixthResult.SuccessOrThrow(),
                seventhResult.SuccessOrThrow(),
                eigthResult.SuccessOrThrow());
        }
    }
}