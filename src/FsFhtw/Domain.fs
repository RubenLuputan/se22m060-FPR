module Domain
open System.Collections.Immutable
open System.Collections.Generic

type Cat = {
    Name : string
}

type Customer = {
    Name: string
}

type Product = { 
    Name: string
    Customer: Customer
}

type Order = {
    Products : Product list
    Categories: Cat[]
}

type Store = {
    Orders: Order list
}

type Message =
    | Category of list<string> | NoCategory
    | AddOrder of list<string> * Cat list
    | UndoOrder
    | RedoOrder

type State = ImmutableStack<Store>
let private RedoState : Stack<Store> = Stack()

let init () : State = ImmutableStack.Empty

let update (msg: Message) (model: State) : State =
    match msg with
    | AddOrder (input, cats) ->
        let products : Product list = input |> List.map (fun p -> {Name = p; Customer = {Name = "se22m060"}})
        if model.IsEmpty
            then model.Push {Orders = [{Products = products; Categories = cats |> Array.ofList}]}
        else model.Push {Orders = [{Products = products; Categories = cats |> Array.ofList}] @ model.Peek().Orders }
    | UndoOrder ->
        if model.IsEmpty
            then ImmutableStack.Empty
        else
            RedoState.Push (model.Peek())
            model.Pop()
    | RedoOrder ->
        model.Push (RedoState.Pop())
    | _ -> model