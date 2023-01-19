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
    
}

type Order = {
    Products : Product list
    Categories: Cat[]
    Customer: Customer
}

type Store = {
    Orders: Order list
}

type Message =
    | Category of list<string> | NoCategory
    | AddOrder of list<string> * Cat list
    | UndoOrder
    | RedoOrder

//in hindsight state could have been a node along the lines of:
//type private SetTree<'a> = 
//  | Empty
//  | Node of SetTree<'a> * 'a * SetTree<'a>
//So that undo/redo would be even easier, just traversing the tree
type State = ImmutableStack<Store>
let private RedoState : Stack<Store> = Stack()

let init () : State = ImmutableStack.Empty

let update (msg: Message) (model: State) : State =
    match msg with
    | AddOrder (input, cats) ->
        let products : Product list = input |> List.map (fun p -> {Name = p})
        if model.IsEmpty
            then model.Push {Orders = [{Products = products; Categories = cats |> Array.ofList; Customer = {Name = "se22m060"}}]}
        else model.Push {Orders = [{Products = products; Categories = cats |> Array.ofList; Customer = {Name = "se22m060"}}] @ model.Peek().Orders }
    | UndoOrder ->
        if model.IsEmpty
            then 
                printf "Nothing to undo.\n"
                ImmutableStack.Empty
        else
            RedoState.Push (model.Peek())
            model.Pop()
    | RedoOrder ->
        try model.Push (RedoState.Pop())
        with
        | _ ->
            printf "Nothing to redo.\n"
            model
    | _ -> model