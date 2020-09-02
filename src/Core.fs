namespace Fable.BizCharts

// yarn add react-beautiful-dnd

open Fable.Core
open Fable.React
open Fable.Core.JsInterop
open Fable.React.Props

type DnDObject<'T when 'T :> DnDObject<'T>>() =
    let mutable props = []

    member x.JSON = createObj props
    member internal x.attribute name value =
        props <- props @ [name ==> value]
        x :?> 'T

type DnDElement<'T when 'T :> DnDElement<'T>> (partialImport: obj -> ReactElement seq -> ReactElement) =
    inherit DnDObject<'T>()
    
    member x.partialImport = partialImport

    member x.children (children: ReactElement list) =
        partialImport x.JSON children

    member x.build () =
        partialImport x.JSON []

    // Common Attributes
    member x.set (v: string * obj) = match v with (name, value) -> x.attribute name value
    member x.style (css: CSSProp list) = x.attribute "style" (keyValueList CaseRules.LowerFirst css)
    member x.id (v: string) = x.attribute "id" v
    member x.key (v: string) = x.attribute "key" v

