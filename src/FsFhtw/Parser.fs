module Parser

open System

let safeEquals (it : string) (theOther : string) =
    String.Equals(it, theOther, StringComparison.OrdinalIgnoreCase)

[<Literal>]
let HelpLabel = "Help"
 
let (|AddOrder|UndoOrder|RedoOrder|Help|ParseFailed|) (input : string)=
    let parts = input.Split(' ') |> List.ofArray |> List.ofSeq
    match parts with
    | verb :: args when safeEquals verb (nameof Domain.AddOrder) ->
        let isCategory (s : string) =  safeEquals s (nameof Domain.Category)
        // find first occurance which is category verb
        let categoryVerbIndex = args |> List.tryFindIndex isCategory
        match categoryVerbIndex with
        | None -> 
            AddOrder(args, None)
        | Some index -> 
            if index + 1 < List.length args then
                let categoryArgs = List.skip (index + 1) args
                let filteredArgs = Seq.takeWhile (fun (i: string) -> i.ToLower() = "category" |> not) args
                AddOrder(filteredArgs |> List.ofSeq, Some categoryArgs)
            else 
                ParseFailed
    | [ verb ] when safeEquals verb (nameof Domain.UndoOrder) -> UndoOrder
    | [ verb ] when safeEquals verb (nameof Domain.RedoOrder) -> RedoOrder
    | [ verb ] when safeEquals verb HelpLabel -> Help
    | _ -> Help
