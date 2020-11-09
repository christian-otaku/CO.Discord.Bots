namespace FSharp.Core

[<Struct>]
type Effect<'env, 'out> = 
    | Effect of ('env -> 'out)

[<RequireQualifiedAccess; CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Effect =
    let inline value x : Effect<'env, 'out> = Effect (fun _ -> x)

    let inline apply fn : Effect<'env, 'out> = Effect fn

    let run (env : 'env) (Effect fn) : 'out = fn env

    let inline bind (fn : 'a -> Effect<'env, 'b>) effect =
        (fun env ->
            let x = run env effect
            run env (fn x))
        |> Effect

    let inline map (fn : 'a -> 'b) (effect : Effect<'env, 'a>) =
        (fun env ->
            let x = run env effect
            fn x)
        |> Effect

[<Struct>]
type EffectBuilder =
    member inline __.Return (value) : Effect<'env, 'out> =
        Effect.value value

    member inline __.Zero () : Effect<'env, 'out> =
        Effect.value (Unchecked.defaultof<_>)

    member inline __.ReturnFrom (effect) : Effect<'env, 'out> =
        effect

    member inline __.Bind (effect : Effect<'env, 'a>, fn : 'a -> Effect<'env, 'b>) =
        Effect.bind fn effect

[<AutoOpen>]
module EffectBuilderImpl =
    let effect = EffectBuilder()