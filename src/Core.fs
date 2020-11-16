namespace Fable.ReactKanban

open Fable.Core
open Fable.React
open Fable.Core.JsInterop
open Fable.React.Props

type KanbanObject<'T when 'T :> KanbanObject<'T>>() =
    let mutable props = []

    member x.JSON = createObj props

    member internal x.attribute name value =
        props <- props @ [ name ==> value ]
        x :?> 'T

type KanbanElement<'T when 'T :> KanbanElement<'T>>(partialImport: obj -> ReactElement seq -> ReactElement) =
    inherit KanbanObject<'T>()

    member x.Item
        with get (children: ReactElement list) = partialImport x.JSON children

    // Common Attributes
    member x.set(v: string * 'TValue) =
        match v with
        | (name, value) -> x.attribute name value

    member x.style(css: CSSProp list) =
        x.attribute "style" (keyValueList CaseRules.LowerFirst css)

    member x.id(v: string) = x.attribute "id" v
    member x.key(v: string) = x.attribute "key" v

