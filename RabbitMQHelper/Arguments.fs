namespace RabbitMQHelper

module Arguments =
    open Argu

    type ExchangeType =
        | Direct = 0
        | Fanout = 1
        | Topic = 2
        
    type QueueType =
        | Classic = 0
        | Quorum = 1
        
    type CreateExchangeArgs =
        | [<Unique; Mandatory>] Name of string
        | [<Unique>] Type of ExchangeType
        
        interface IArgParserTemplate with
            member s.Usage =
                match s with
                | Name _ -> "specify a name for the exchange."
                | Type _ -> "specify a type of the exchange."
                
    type CreateQueueArgs =
        | [<Unique; Mandatory>] Name of string
        | [<Unique>] Type of QueueType
        | [<Unique>] Durable of bool
        
        interface IArgParserTemplate with
            member s.Usage =
                match s with
                | Name _ -> "specify a name for the queue."
                | Type _ -> "specify a type of the queue."
                | Durable _ -> "specify if a queue is durable."

    type CreateArgs =
        | [<CliPrefix(CliPrefix.None)>] Exchange of ParseResults<CreateExchangeArgs>
        | [<CliPrefix(CliPrefix.None)>] Queue of ParseResults<CreateQueueArgs>
        
        interface IArgParserTemplate with
            member s.Usage =
                match s with
                | Exchange _ -> "create an exchange."
                | Queue _ -> "create a queue."

    type DeleteExchangeArgs =
        | [<Unique; Mandatory>] Name of string
        
        interface IArgParserTemplate with
            member s.Usage =
                match s with
                | Name _ -> "specify a name for the exchange."
                
    type DeleteQueueArgs =
        | [<Unique; Mandatory>] Name of string
        
        interface IArgParserTemplate with
            member s.Usage =
                match s with
                | Name _ -> "specify a name for the queue."
                
    type DeleteArgs =
        | [<CliPrefix(CliPrefix.None)>] Exchange of ParseResults<DeleteExchangeArgs>
        | [<CliPrefix(CliPrefix.None)>] Queue of ParseResults<DeleteQueueArgs>
        
        interface IArgParserTemplate with
            member s.Usage =
                match s with
                | Exchange _ -> "delete an exchange."
                | Queue _ -> "delete a queue."
                
    type BindQueueArgs =
        | [<Unique; Mandatory>] Exchange of string
        | [<Unique; Mandatory>] Queue of string
        | [<Unique>] RoutingKey of string
        
        interface IArgParserTemplate with
            member s.Usage =
                match s with
                | Exchange _ -> "specify a name of the exchange to bind the queue to."
                | Queue _ -> "specify a name of the queue to bind to the exchange."
                | RoutingKey _ -> "specify a routing key for the queue."
                
    type UnBindQueueArgs =
        | [<Unique; Mandatory>] Exchange of string
        | [<Unique; Mandatory>] Queue of string
        | [<Unique>] RoutingKey of string
        
        interface IArgParserTemplate with
            member s.Usage =
                match s with
                | Exchange _ -> "specify a name of the exchange to unbind the queue from."
                | Queue _ -> "specify a name of the queue to unbind from the exchange."
                | RoutingKey _ -> "specify a routing key for the queue."
        
    type CliArgs =
        | [<Unique; Mandatory; AltCommandLine("-u")>] User of user: string
        | [<Unique; Mandatory; AltCommandLine("-p")>] Pass of pass: string
        | [<CliPrefix(CliPrefix.None)>] Create of ParseResults<CreateArgs>
        | [<CliPrefix(CliPrefix.None)>] Delete of ParseResults<DeleteArgs>
        | [<CliPrefix(CliPrefix.None)>] Bind of ParseResults<BindQueueArgs>
        | [<CliPrefix(CliPrefix.None)>] UnBind of ParseResults<BindQueueArgs>

        interface IArgParserTemplate with
            member s.Usage =
                match s with
                | User _ -> "username to use."
                | Pass _ -> "password to use."
                | Create _ -> "create an exchange or queue."
                | Delete _ -> "delete an exchange or queue."
                | Bind _ -> "bind the queue to the exchange."
                | UnBind _ -> "unbind the queue from the exchange."
