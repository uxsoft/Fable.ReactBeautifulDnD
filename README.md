# Fable.ReactKanban

React Kanban bindings for Fable React

<img src="https://buildstats.info/nuget/Fable.ReactKanban" alt="badge"/>

## Usage 

### Dependencies:

`yarn add @lourenci/react-kanban`

`dotnet package add Fable.ReactKanban`

### Fable

```fsharp
KanbanBoard<KBoard, KColumn, KCard>()
    .disableColumnDrag()
    .style([ Width "100%"; Height "3000px" ])
    .onCardDragEnd(fun card source destination ->
        onChangeStatus app card.id destination.toColumnId)
    .renderCard(fun (card) props ->
        div [] [ str card.title ])
    .children(mapBoard app level)
    .[[]]
```