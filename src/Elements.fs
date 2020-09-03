namespace Fable.ReactBeautifulDnD

open Browser.Types
open Fable.Core
open Fable.React

[<StringEnum; RequireQualifiedAccess>]
type MovementMode = | FLUID | SNAP

type BeforeCapture = {|
  draggableId: string
  mode: MovementMode
|}

type DraggableLocation = {|
  droppableId: string
  index: int
|}

type DragStart = {|
  draggableId: string
  ``type``: string
  source: DraggableLocation
  mode: MovementMode
|}

type ResponderProvided = {|
  announce: string -> unit
|};

type Combine = {|
  draggableId: string
  droppableId: string
|}

type DragUpdate = {|
  draggableId: string
  ``type``: string
  source: DraggableLocation
  mode: MovementMode
  destination: DraggableLocation option
  combine: Combine option
|}

type DropReason = | DROP | CANCEL

type DropResult = {|
  draggableId: string
  ``type``: string
  source: DraggableLocation
  mode: MovementMode
  destination: DraggableLocation option
  combine: Combine option
  reason: DropReason
|}
        
type DroppableMode = | Standard | Virtual
type Direction = | Horizontal | Vertical
  
type DroppableProps = {|
  ``data-rbd-droppable-context-id``: string
  ``data-rbd-droppable-id``: string
|};
    
type DroppableProvided = {|
  innerRef: HTMLElement -> unit
  droppableProps: DroppableProps
  placeholder: ReactElement
|}

type DroppableStateSnapshot = {|
  isDraggingOver: bool
  draggingOverWith: string
  draggingFromThisWith: string
  isUsingPlaceholder: bool
|}

type DraggableProps = {|
  style: obj
  ``data-rbd-draggable-context-id``: string
  ``data-rbd-draggable-id``: string
  onTransitionEnd: TransitionEvent -> unit
|}

type DragHandleProps = {|
  ``data-rbd-drag-handle-draggable-id``: string
  ``data-rbd-drag-handle-context-id``: string
  ``aria-labelledby``: string
  tabIndex: int
  draggable: bool
  onDragStart: DragEvent -> unit
|}

type DraggableProvided = {|
  innerRef: (HTMLElement) -> unit
  draggableProps: DraggableProps
  dragHandleProps: DragHandleProps
|}

type DraggableRubric = {|
  draggableId: string
  ``type``: string
  source: DraggableLocation
|};        

type DraggableStateSnapshot = {|
  isDragging: bool
  isDropAnimating: bool
  dropAnimation: obj option
  draggingOver: string
  combineWith: string
  combineTargetFor: string
  mode: MovementMode option
|}

type DraggableChildrenFn = DraggableProvided -> DraggableStateSnapshot -> DraggableRubric -> ReactElement

type DragDropContext(onDragEnd: DropResult -> ResponderProvided -> unit) as x =
    inherit DnDElement<DragDropContext>(ofImport "DragDropContext" "react-beautiful-dnd")
    do x.attribute "onDragEnd" onDragEnd |> ignore
    member x.onBeforeCapture (v: BeforeCapture -> unit ) = x.attribute "onBeforeCapture" v
    member x.onBeforeDragStart (v: DragStart -> unit) = x.attribute "onBeforeDragStart" v
    member x.onDragStart (v: DragStart -> ResponderProvided -> unit) = x.attribute "onDragStart" v
    member x.onDragUpdate (v: DragUpdate -> ResponderProvided -> unit) = x.attribute "onDragUpdate" v
    member x.Item
        with get(children: ReactElement list) = x.partialImport x.JSON (List.toSeq children)

type Droppable(droppableId: string) as x =
    inherit DnDElement<Droppable>(ofImport "Droppable" "react-beautiful-dnd")
    do x.attribute "droppableId" droppableId |> ignore
    member x.droppableType (v: string) = x.attribute "type" v
    member x.mode (v: DroppableMode) = x.attribute "mode" v
    member x.isDropDisabled (v: bool) = x.attribute "isDropDisabled" v
    member x.isCombineEnabled (v: bool) = x.attribute "isCombineEnabled" v
    member x.direction (v: Direction) = x.attribute "direction" v
    member x.ignoreContainerClipping (v: bool) = x.attribute "ignoreContainerClipping" v
    member x.renderClone (v: DraggableChildrenFn) = x.attribute "renderClone" v
    member x.getContainerForClone (v: unit -> HTMLElement) = x.attribute "getContainerForClone" v
    member x.Item
        with get(children: DroppableProvided -> DroppableStateSnapshot -> ReactElement list) =
          x.attribute "children" children |> ignore
          x.partialImport x.JSON Seq.empty

type Draggable(draggableId: string, index: int) as x =
    inherit DnDElement<Draggable>(ofImport "Draggable" "react-beautiful-dnd")
    do
      x.attribute "draggableId" draggableId |> ignore
      x.attribute "index" index |> ignore
    member x.isDragDisabled (v: bool) = x.attribute "isDragDisabled" v
    member x.disableInteractiveElementBlocking (v: bool) = x.attribute "disableInteractiveElementBlocking" v
    member x.shouldRespectForcePress (v: bool) = x.attribute "shouldRespectForcePress" v
    member x.Item
        with get(children: DraggableProvided -> DraggableStateSnapshot -> ReactElement list) =
          x.attribute "children" children |> ignore
          x.partialImport x.JSON Seq.empty