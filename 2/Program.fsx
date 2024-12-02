open System.IO

let data =
    File.ReadAllLines("2/input.txt")
    |> Array.map (fun line -> line.Split ' ' |> Array.map int)

let findPrefixDiff = fun arr ->
    Array.windowed 2 arr
    |> Array.map (fun p -> p[1] - p[0])

let prefixDiff = data |> Array.map findPrefixDiff

let check = fun report -> 
    (Array.forall (fun e -> e > 0) report 
        || Array.forall (fun e -> e < 0) report
    ) 
    && Array.forall (fun e -> (abs e) >= 1 && (abs e) <= 3) report

let count =
    prefixDiff
    |> Array.fold (fun acc item -> if check item then acc + 1 else acc) 0

printfn $"{count}"

// -----------------

let removedPrefixDiff =
    data
    |> Array.map
        (fun item ->
            [|0..item.Length - 1|]
            |> Array.map (fun i -> Array.removeAt i item)
            |> Array.map findPrefixDiff
        )

let count2 =
    Array.zip prefixDiff removedPrefixDiff
    |> Array.fold
        (fun acc (full, rem) ->
            if check full || Array.exists check rem
            then acc + 1
            else acc
        )
        0

printfn $"{count2}"