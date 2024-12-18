open System.IO

let data = File.ReadAllLines("5/input.txt");
let spec =
    data
    |> Array.filter _.Contains('|')
    |> Array.map
        (fun e ->
            let sp = e.Split('|')
            (int sp[0], int sp[1])
        )
    |> Array.fold
        (fun acc (a, b) ->
            Map.change
                b
                (function
                | Some x -> Some (Set.add a x)
                | None -> Some (Set.add a Set.empty)
                )
                acc
        )
        Map.empty

let count =
    data
    |> Array.filter _.Contains(',')
    |> Array.fold
        (fun acc e ->
            let nums = e.Split(',') |> Array.map int
            let numSet = Set.ofArray nums
            let valid = 
                (nums 
                |> Array.map
                    (fun e -> (Set.intersect ((Map.tryFind e spec) |> Option.defaultValue Set.empty) numSet).Count)
                ) = [|0..nums.Length - 1|] 

            if valid then acc + (nums[nums.Length / 2]) else acc
        )
        0

printfn "%d" count

// -----------------

let count2 =
    data
    |> Array.filter _.Contains(',')
    |> Array.fold
        (fun acc e ->
            let nums = e.Split(',') |> Array.map int
            let numSet = Set.ofArray nums
            let valid = 
                (nums 
                |> Array.map
                    (fun e -> (Set.intersect ((Map.tryFind e spec) |> Option.defaultValue Set.empty) numSet).Count)
                ) = [|0..nums.Length - 1|] 

            let x () =
                nums 
                |> Array.map
                    (fun e ->
                        (e, (Set.intersect ((Map.tryFind e spec) |> Option.defaultValue Set.empty) numSet).Count)
                    )
                |> Array.sortBy snd
                |> Array.map fst
                |> (fun e -> e[e.Length / 2])
                
            if not valid then acc + x () else acc
        )
        0

printfn "%d" count2