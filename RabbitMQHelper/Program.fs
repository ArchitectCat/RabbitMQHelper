namespace RabbitMQHelper

module Program =
    open Argu
    open Arguments
    open System
    
    [<EntryPoint>]
    let main (argv: string []): int =
        let errorHandler = ProcessExiter(colorizer = function ErrorCode.HelpText -> None | _ -> Some ConsoleColor.Red)
        let parser = ArgumentParser.Create<CliArgs>(programName = "RabbitMQHelper.exe", errorHandler = errorHandler)
        let results = parser.ParseCommandLine argv
        
        if results.GetAllResults().Length = 0 then
            let usage = parser.PrintUsage()
            Console.Write(usage)
        else
            ()
        0 // return an integer exit code