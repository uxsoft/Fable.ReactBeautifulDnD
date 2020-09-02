namespace Fable.BizCharts

open Browser.Types
open Fable.Core
open Fable.React
open Fable.Core.JsInterop

type BizCanvas() =
    inherit BizElement<BizCanvas>(ofImport "Canvas" "bizcharts")
    member x.renderer (v: string) = x.attribute "renderer" v
    member x.width (v: int) = x.attribute "width" v
    member x.height (v: int) = x.attribute "height" v
    member x.container (v: HTMLElement) = x.attribute "container" v
    member x.cursor (v: string) = x.attribute "cursor" v
    
type BizGroup() =
    inherit BizElement<BizGroup>(ofImport "Group" "bizcharts")