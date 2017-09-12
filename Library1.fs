namespace FSharpDll

open Microsoft.Azure.WebJobs
open System.Net
open System.Net.Http
open Microsoft.Azure.WebJobs.Host
open FSharp.Data

type Class1() = 
    member this.X = "F#"


    static member Runn(req: HttpRequestMessage, log: TraceWriter) =
        async {
            log.Info("start")

            let result = FSharp.Data.HtmlDocument.Load("http://www.google.com")
            log.Info("sync result")
            log.Info(string(result.ToString().Length))

            System.AppDomain.CurrentDomain.GetAssemblies()
            |> Seq.map (sprintf "%A")
            |> Seq.iter log.Info

            try
                let! result = HtmlDocument.AsyncLoad("http://www.google.com")
                log.Info("async result")
                log.Info(string (result.ToString().Length))
            with 
            | e -> log.Error("failed on reflection call", e)

            log.Info("finished")
        } |> Async.StartAsTask
