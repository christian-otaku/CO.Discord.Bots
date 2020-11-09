namespace FSharp.Control

open System.Threading.Tasks

[<AutoOpen>]
module AsyncExtensions =
    type AsyncBuilder with
        member inline __.ReturnFrom (computation : Task<'T>) =
            Async.AwaitTask computation

        member inline __.ReturnFrom (computation : Task) =
            Async.AwaitTask computation

        member inline __.Combine (computation1 : Task, computation2 : Task<'T>) = 
            async.Combine (Async.AwaitTask computation1, Async.AwaitTask computation2)

        member inline __.Combine (computation1 : Task, computation2 : Task) = 
            async.Combine (Async.AwaitTask computation1, Async.AwaitTask computation2)

        member inline __.Combine (computation1 : Task<unit>, computation2 : Task<'T>) =
            async.Combine (Async.AwaitTask computation1, Async.AwaitTask computation2)

        member inline __.Combine (computation1 : Task<unit>, computation2 : Task) =
            async.Combine (Async.AwaitTask computation1, Async.AwaitTask computation2)

        member inline __.Combine (computation1 : Task, computation2 : Async<'T>) =
            async.Combine (Async.AwaitTask computation1, computation2)

        member inline __.Combine (computation1 : Task<unit>, computation2 : Async<'T>) =
            async.Combine (Async.AwaitTask computation1, computation2)

        member inline __.Combine (computation1, computation2 : Task<'T>) =
            async.Combine (computation1, Async.AwaitTask computation2)

        member inline __.Combine (computation1, computation2 : Task) =
            async.Combine (computation1, Async.AwaitTask computation2)

        member __.Delay (generator : unit -> Task<'T>) =
            async.Delay (fun () -> Async.AwaitTask (generator ()))

        member __.Delay (generator : unit -> Task) =
            async.Delay (fun () -> Async.AwaitTask (generator ()))

        member __.Using (resource, binder : 'T -> Task<'U>) =
            async.Using (resource, binder >> Async.AwaitTask)

        member __.Using (resource, binder : 'T -> Task) =
            async.Using (resource, binder >> Async.AwaitTask)

        member __.While (guard, computation : Task<unit>) =
            async.While (guard, Async.AwaitTask computation)

        member __.While (guard, computation : Task) =
            async.While (guard, Async.AwaitTask computation)

        member __.For (sequence, body : 'T -> Task<unit>) =
            async.For (sequence, body >> Async.AwaitTask)

        member __.For (sequence, body : 'T -> Task) =
            async.For (sequence, body >> Async.AwaitTask)

        member inline __.TryFinally (computation : Task<'T>, compensation) =
            async.TryFinally(Async.AwaitTask computation, compensation)

        member inline __.TryFinally (computation : Task, compensation) =
            async.TryFinally(Async.AwaitTask computation, compensation)

        member inline __.TryWith (computation : Task<'T>, catchHandler : exn -> Task<'T>) =
            async.TryWith (Async.AwaitTask computation, catchHandler >> Async.AwaitTask)

        member inline __.TryWith (computation : Task, catchHandler : exn -> Task<unit>) =
            async.TryWith (Async.AwaitTask computation, catchHandler >> Async.AwaitTask)

        member inline __.TryWith (computation : Task, catchHandler : exn -> Task) =
            async.TryWith (Async.AwaitTask computation, catchHandler >> Async.AwaitTask)

        member inline __.TryWith (computation : Task<unit>, catchHandler : exn -> Task<unit>) =
            async.TryWith (Async.AwaitTask computation, catchHandler >> Async.AwaitTask)

        member inline __.TryWith (computation : Task<unit>, catchHandler : exn -> Task) =
            async.TryWith (Async.AwaitTask computation, catchHandler >> Async.AwaitTask)