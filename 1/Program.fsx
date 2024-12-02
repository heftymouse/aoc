open System.IO

let a, b =
    File.ReadAllLines("1/input.txt")
    |> Array.toList
    |> List.map
        (fun line ->
            let sp = line.Split "   "
            (int sp[0], int sp[1])
        )
    |> List.unzip
    |> fun (x, y) -> (List.sort x, List.sort y)

let count = List.fold2 (fun acc x y -> acc + abs (x - y)) 0 a b

printfn $"{count}"

// -----------------

let bMap =
    b 
    |> List.fold 
        (fun (acc: Map<int,int>) i -> 
            acc.Change(i,
                function
                    | Some x -> Some (x + 1)
                    | None -> Some 1
                )
        )
        Map.empty

let count2 =
    List.fold
        (fun acc x -> 
            acc + (x * (bMap.TryFind x |> Option.defaultValue 0))
        )
        0
        a 

printfn $"{count2}"