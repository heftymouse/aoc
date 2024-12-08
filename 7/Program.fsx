open System.IO

let data = 
    File.ReadAllLines("7/input.txt")
    |> Array.map
        (fun e ->
            let sp = e.Split(": ")
            (
                int64 sp[0], 
                sp[1].Split(' ') |> Array.map int64 |> List.ofArray
            )
        )

type Operator =
    Add
    | Multiply
    | Join

let rec combinations n = 
    match n with
    | 0 -> [[]]
    | _ -> (
        let prev = combinations (n - 1)
        [for i in prev do Add :: i] @ [for i in prev do Multiply :: i]
    )

let rec doExpression init (nums:int64 list) (ops:Operator list) =
    match nums.Length with
    | 0 -> init
    | _ -> (
        match ops.Head with
        | Add -> doExpression (init + nums.Head) nums.Tail ops.Tail
        | Multiply -> doExpression (init * nums.Head) nums.Tail ops.Tail
        | _   -> doExpression init nums.Tail ops.Tail
    )
    

let count =
    data
    |> Array.fold
        (fun acc (target, nums) ->
            let results = 
                combinations (nums.Length - 1)
                |> List.map (fun e -> doExpression nums.Head nums.Tail e)

            if List.exists (fun e -> e = target) results
            then acc + target
            else acc
        )
        0L

printfn "%d" count

// -----------------

let rec combinations2 n = 
    match n with
    | 0 -> [[]]
    | _ -> (
        let prev = combinations2 (n - 1)
        [for i in prev do Add :: i] @ [for i in prev do Multiply :: i] @ [for i in prev do Join :: i]
    )

let rec doExpression2 (init:int64) (nums:int64 list) (ops:Operator list) =
    if nums.Length = 0
    then init
    else
    match ops.Head with
    | Add -> doExpression2 (init + nums.Head) nums.Tail ops.Tail
    | Multiply -> doExpression2 (init * nums.Head) nums.Tail ops.Tail
    | Join -> doExpression2 (int64 $"{init}{nums.Head}") nums.Tail ops.Tail

let count2 =
    data
    |> Array.fold
        (fun acc (target, nums) ->
            let results = 
                combinations2 (nums.Length - 1)
                |> List.map (fun e -> doExpression2 nums.Head nums.Tail e)

            if List.exists (fun e -> e = target) results
            then acc + target
            else acc
        )
        0L

printfn "%d" count2
