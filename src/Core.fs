namespace Fable.BizCharts

// yarn add bizcharts

open System
open Fable.Core
open Fable.React
open Fable.Core.JsInterop
open Fable.React.Props

type BizObject<'T when 'T :> BizObject<'T>>() =
    let mutable props = []

    member x.JSON = createObj props
    member internal x.attribute name value =
        props <- props @ [name ==> value]
        x :?> 'T

type BizElement<'T when 'T :> BizElement<'T>> (partialImport: obj -> ReactElement seq -> ReactElement) =
    inherit BizObject<'T>()

    member x.Item
        with get(children: ReactElement list) = partialImport x.JSON children

    member x.children (children: ReactElement list) =
        partialImport x.JSON children

    member x.build () =
        partialImport x.JSON []

    // Common Attributes
    member x.set (v: string * obj) = match v with (name, value) -> x.attribute name value
    member x.key (v: string) = x.attribute "key" v

[<StringEnum; RequireQualifiedAccess>]
type InteractionType =
    | [<CompiledName("active-region")>] ActiveRegion
    | [<CompiledName("view-zoom")>] ViewZoom
    | [<CompiledName("element-active")>] ElementActive
    | [<CompiledName("element-selected")>] ElementSelected
    | [<CompiledName("element-single-selected")>] ElementSingleSelected
    | [<CompiledName("element-highlight")>] ElementHighlight
    | [<CompiledName("element-highlight-by-x")>] ElementHighlightByX
    | [<CompiledName("element-highlight-by-color")>] ElementHighlightByColor
    | [<CompiledName("legend-filter")>] LegendFilter
    | [<CompiledName("legend-visible-filter")>] LegendVisibleFilter
    | [<CompiledName("continuous-filter")>] ContinuousFilter
    | [<CompiledName("continuous-visible-filter")>] ContinuousVisibleFilter
    | [<CompiledName("legend-active")>] LegendActive
    | [<CompiledName("legend-highlight")>] LegendHighlight
    | [<CompiledName("axis-label-highlight")>] AxisLabelHighlight
    | [<CompiledName("element-list-highlight")>] ElementListHighlight
    | [<CompiledName("brush")>] Brush
    | [<CompiledName("brush-x")>] BrushX
    | [<CompiledName("brush-y")>] BrushY
    | [<CompiledName("brush-visible")>] BrushVisible

type BizChart<'TData>(data: 'TData array) as this =
    inherit BizElement<BizChart<'TData>>(ofImport "Chart" "bizcharts")
    do this.attribute "data" data |> ignore
    member x.autoFit (v: bool) = x.attribute "autoFit" v
    member x.width (v: int) = x.attribute "width" v
    member x.height (v: int) = x.attribute "height" v
    member x.padding (v: int array) = x.attribute "padding" v
    member x.appendPadding (v: int) = x.attribute "appendPadding" v
    member x.pixelRatio (v: int) = x.attribute "pixelRatio" v
    member x.pure (v: bool) = x.attribute "pure" v
    member x.errorContent (v: string) = x.attribute "errorContent" v
    member x.placeholder (v: bool) = x.attribute "placeholder" v
    member x.defaultInteractions (v: InteractionType array) = x.attribute "defaultInteractions" v
    member x.interactions (v: InteractionType array) = x.attribute "interactions" v
    member x.animate (v: bool) = x.attribute "animate" v
    member x.filter (v: obj array) = x.attribute "filter" v
    member x.scale (v: obj) = x.attribute "scale" v

type BizView() =
    inherit BizElement<BizView>(ofImport "View" "bizcharts")
    member x.region (v: obj) = x.attribute "region" v
    member x.data (v: obj array) = x.attribute "data" v
    member x.scale (v: obj) = x.attribute "scale" v
    member x.padding (v: int array) = x.attribute "padding" v
    member x.animate (v: bool) = x.attribute "animate" v

type BizAxis() =
    inherit BizElement<BizAxis>(ofImport "Axis" "bizcharts")
    member x.name (v: string) = x.attribute "name" v
    member x.visible (v: bool) = x.attribute "visible" v
    member x.position (v: string) = x.attribute "position" v
    member x.title (v: bool) = x.attribute "title" v
    member x.line (v: obj) = x.attribute "line" v
    member x.tickLine (v: obj) = x.attribute "tickLine" v
    member x.label (v: obj) = x.attribute "label" v
    member x.subTickLine (v: obj) = x.attribute "subTickLine" v
    member x.animate (v: bool) = x.attribute "animate" v
    member x.verticalFactor (v: int) = x.attribute "verticalFactor" v

[<StringEnum; RequireQualifiedAccess>]
type LegendPosition =
    | [<CompiledName("left")>] Left
    | [<CompiledName("left-top")>] LeftTop
    | [<CompiledName("left-bottom")>] LeftBottom
    | [<CompiledName("right")>] Right
    | [<CompiledName("right-top")>] RightTop
    | [<CompiledName("right-bottom")>] RightBottom
    | [<CompiledName("top")>] Top
    | [<CompiledName("top-left")>] TopLeft
    | [<CompiledName("top-right")>] TopRight
    | [<CompiledName("bottom")>] Bottom
    | [<CompiledName("bottom-left")>] BottomLeft
    | [<CompiledName("bottom-right")>] BottomRight

[<StringEnum; RequireQualifiedAccess>]
type LegendLayout =
    | Horizontal
    | Vertical

type BizLegend() =
    inherit BizElement<BizLegend>(ofImport "Legend" "bizcharts")
    member x.name (v: string) = x.attribute "name" v
    member x.visible (v: bool) = x.attribute "visible" v
    member x.position (v: LegendPosition) = x.attribute "position" v
    member x.offsetX (v: int) = x.attribute "offsetX" v
    member x.offsetY (v: int) = x.attribute "offsetY" v
    member x.layout (v: LegendLayout) = x.attribute "layout" v
    member x.title (v: bool) = x.attribute "title" v
    member x.background (v: obj) = x.attribute "background" v
    member x.filter (v: Func<'TData, DateTime, int, bool>) = x.attribute "filter" v
    member x.onChange (v: Func<Browser.Types.Event, obj, unit>) = x.attribute "onChange" v
    member x.slidable (v: bool) = x.attribute "slidable" v
    member x.min (v: float) = x.attribute "min" v
    member x.max (v: float) = x.attribute "max" v
    member x.value (v: float) = x.attribute "value" v
    member x.track (v: obj) = x.attribute "track" v
    member x.rail (v: obj) = x.attribute "rail" v
    member x.handler (v: obj) = x.attribute "handler" v
    member x.custom (v: bool) = x.attribute "custom" v
    member x.items (v: obj array) = x.attribute "custom" v
    member x.itemSpacing (v: int) = x.attribute "itemSpacing" v
    member x.itemWidth (v: int) = x.attribute "itemWidth" v
    member x.itemHeight (v: int) = x.attribute "itemHeight" v
    member x.itemValue (v: obj) = x.attribute "itemValue" v
    member x.itemName (v: obj) = x.attribute "itemName" v
    member x.maxWidth (v: int) = x.attribute "maxWidth" v
    member x.maxHeight (v: int) = x.attribute "maxHeight" v
    member x.marker (v: obj) = x.attribute "marker" v
    member x.flipPage (v: bool) = x.attribute "flipPage" v
    member x.reversed (v: bool) = x.attribute "reversed" v

[<StringEnum; RequireQualifiedAccess>]
type CoordinateType =
    | Rect | Cartesian | Polar | Theta | Helix

type BizCoordinate() =
    inherit BizElement<BizCoordinate>(ofImport "Coordinate" "bizcharts")
    member x.radius (v: float) = x.attribute "radius" v
    member x.innerRadius (v: float) = x.attribute "innerRadius" v
    member x.startAngle (v: float) = x.attribute "startAngle" v
    member x.endAngle (v: float) = x.attribute "endAngle" v
    member x.coordinateType (v: CoordinateType) = x.attribute "type" v
    member x.rotate (v: float) = x.attribute "rotate" v
    member x.scale (v: float array) = x.attribute "scale" v
    member x.reflect (v: string) = x.attribute "reflect" v
    member x.transpose (v: bool) = x.attribute "transpose" v
    member x.actions (v: obj array) = x.attribute "actions" v

[<StringEnum; RequireQualifiedAccess>]
type TooltipPosition =
    | Left
    | Right
    | Top
    | Bottom

type BizTooltip() =
    inherit BizElement<BizCoordinate>(ofImport "Tooltip" "bizcharts")
    member x.showTitle (v: bool) = x.attribute "showTitle" v
    member x.title (v: string) = x.attribute "title" v
    member x.showMarkers (v: bool) = x.attribute "showMarkers" v
    member x.marker (v: obj) = x.attribute "marker" v
    member x.showContent (v: bool) = x.attribute "showContent" v
    member x.position (v: TooltipPosition) = x.attribute "position" v
    member x.shared (v: bool) = x.attribute "shared" v
    member x.follow (v: bool) = x.attribute "follow" v
    member x.offset (v: float) = x.attribute "offset" v
    member x.enterable (v: bool) = x.attribute "enterable" v
    member x.showCrosshairs (v: bool) = x.attribute "showCrosshairs" v
    member x.crosshairs (v: obj) = x.attribute "crosshairs" v


// TrendCfg
//{
//  data: number[], // 背景图显示的数据
//  smooth?: boolean, // 是否平滑
//  isArea?: boolean, // 是否显示区域
//  backgroundStyle?: object, // 背景图样式
//  lineStyle?: object, // 线条样式
//  areaStyle?: object, // 数据区域样式
//}

type BizSlider() =
    inherit BizElement<BizSlider>(ofImport "Slider" "bizcharts")
    member x.height (v: int) = x.attribute "height" v
    member x.trendCfg (v: obj) = x.attribute "trendCfg" v // TODO TrendCfg
    member x.backgroundStyle (v: obj) = x.attribute "backgroundStyle" v
    member x.foregroundStyle (v: obj) = x.attribute "foregroundStyle" v
    member x.handlerStyle (v: obj) = x.attribute "handlerStyle" v
    member x.textStyle (v: obj) = x.attribute "textStyle" v
    member x.minLimit (v: float) = x.attribute "minLimit" v
    member x.maxLimit (v: float) = x.attribute "maxLimit" v
    member x.start (v: obj) = x.attribute "start" v
    member x.``end`` (v: obj) = x.attribute "end" v
    member x.formatter (v: Func<obj, DateTime, float, obj>) = x.attribute "formatter" v

type BizInteraction() =
    inherit BizElement<BizInteraction>(ofImport "Interaction" "bizcharts")
    member x.interactionType (v: InteractionType) = x.attribute "type" v
    member x.config (v: obj) = x.attribute "config" v

[<StringEnum; RequireQualifiedAccess>]
type FacetType =
    | Rect | List | Circle | Tree | Mirror | Matrix

//{
//    offsetX?: number,    /** x 方向偏移。 */
//    offsetY?: number,    /** y 方向偏移。 */
//    style?: object    /** 文本样式。 */
//  }

type BizFacet() =
    inherit BizElement<BizFacet>(ofImport "Facet" "bizcharts")
    member x.facetType (v: FacetType) = x.attribute "type" v
    member x.fields (v: string array) = x.attribute "fields" v
    member x.eachView (v: obj -> unit) = x.attribute "eachView" v // TODO better
    member x.padding (v: int array) = x.attribute "padding" v
    member x.showTitle (v: bool) = x.attribute "showTitle" v // TODO style
    member x.columnTitle (v: obj) = x.attribute "columnTitle" v // TODO style
    member x.rowTitle (v: obj) = x.attribute "rowTitle" v
    member x.cols (v: int) = x.attribute "cols" v
    member x.title (v: obj) = x.attribute "title" v // TODO style
    member x.transpose (v: bool) = x.attribute "transpose" v
    member x.line (v: obj) = x.attribute "line" v
