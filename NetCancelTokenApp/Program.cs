CancellationTokenSource tokenSource = new CancellationTokenSource();
CancellationToken token = tokenSource.Token;

/*
Task task = new Task(() =>
{
    int i = 0;
    token.Register(() =>
    {
        Console.WriteLine("task canceled");
        i = 10;
    });
    for (; i < 10; i++)
    {
        
        //if(token.IsCancellationRequested)
        //{
            //Console.WriteLine("task is canceled");
            //return;
            //token.ThrowIfCancellationRequested();
        //}
        Console.WriteLine($"Task work {i}");
        Thread.Sleep(300);
    }
}, token);


task.Start();

Thread.Sleep(1000);
tokenSource.Cancel();
Thread.Sleep(1000);
Console.WriteLine($"Task status: {task.Status}");
tokenSource.Dispose();
*/

/*
try
{
    task.Start();
    Thread.Sleep(1000);
    tokenSource.Cancel();
    task.Wait();
}
catch(AggregateException ex)
{
    foreach (Exception e in ex.InnerExceptions)
        if (e is TaskCanceledException)
            Console.WriteLine("Task canceled");
        else Console.WriteLine(e.Message);
}
finally 
{ 
    tokenSource.Dispose(); 
}
Console.WriteLine($"Task status: {task.Status}");
*/

new Task(() => {
    Thread.Sleep(500);
    tokenSource.Cancel();
}).Start();

try
{
    Parallel.ForEach<int>(new List<int>() { 1, 3, 4, 7, 9 },
                new ParallelOptions() { CancellationToken = token},
                (n) => {
                    Thread.Sleep(1000);
                    Console.WriteLine($"{n} {n * n}"); 
                        
                }
        );
}
catch(OperationCanceledException ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    tokenSource.Dispose();
}
