namespace Fable.ReactKanban

open System
open Fable.React

type KanbanBoard<'TBoard, 'TColumn, 'TCard>() =
    inherit KanbanElement<KanbanBoard<'TBoard, 'TColumn, 'TCard>>(ofImport "default" "@lourenci/react-kanban")
    member x.initialBoard (v: 'TBoard) = x.attribute "initialBoard" v
    member x.onCardDragEnd (v: Func<'TBoard, 'TCard, {| fromColumnId: int; fromPosition: int |}, {| toColumnId: int; toPosition: int |}>) = x.attribute "onCardDragEnd" v
    member x.onColumnDragEnd (v: Func<'TBoard, 'TColumn, {| fromPosition: int |}, {| toPosition: int |}>) = x.attribute "onColumnDragEnd" v
    member x.renderCard (v: Func<'TCard, obj, ReactElement>) = x.attribute "renderCard" v
    member x.renderColumnHeader (v: Func<'TColumn, obj>) = x.attribute "renderColumnHeader" v
    member x.allowAddColumn (?v: bool) = x.attribute "allowAddColumn" (Option.defaultValue true v)
    member x.onNewColumnConfirm (v) = x.attribute "onNewColumnConfirm" v
    member x.onColumnNew (v) = x.attribute "onColumnNew" v
    member x.renderColumnAdder (v) = x.attribute "renderColumnAdder" v
    member x.disableColumnDrag (v) = x.attribute "disableColumnDrag" v
    member x.disableCardDrag (v) = x.attribute "disableCardDrag" v
    member x.allowRemoveColumn (?v: bool) = x.attribute "allowRemoveColumn" (Option.defaultValue true v)
    member x.onColumnRemove (v) = x.attribute "onColumnRemove" v
    member x.allowRenameColumn (?v: bool) = x.attribute "allowRenameColumn" (Option.defaultValue true v)
    member x.onColumnRename (v: Func<'TBoard, 'TColumn, unit>) = x.attribute "onColumnRename" v
    member x.allowRemoveCard (?v: bool) = x.attribute "allowRemoveCard" (Option.defaultValue true v)
    member x.onCardRemove (v: Func<'TBoard, 'TColumn, 'TCard>) = x.attribute "onCardRemove" v
    member x.allowAddCard (?v: bool) = x.attribute "allowAddCard" (Option.defaultValue true v)
    member x.onCardNew (v: Func<'TCard, unit>) = x.attribute "onCardNew" v
    member x.onNewCardConfirm (v) = x.attribute "onNewCardConfirm" v