module Domain
open System.Collections.Immutable

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
    | AddOrder of list<string> * Cat
    | UndoOrder of string

type State = ImmutableStack<Store>
let private RedoState : Store list = []

let init () : State = ImmutableStack.Empty

let update (msg: Message) (model: State) : State =
    // match msg with
    // | AddOrder input & _ ->
    //     if model.IsEmpty
    //         then model.Push {Orders = [input]}
    //     else model.Push {Orders = [input] @ model.Peek().Orders }
    // | _ -> model
    // | UndoOrder -> 
    //     RedoState :: model.Peek()
    //     model.Pop()
    // | RedoOrder ->
    //     model.Push {Products = (RedoState.Pop() :> Store) @ model }
    failwith "not done yet"