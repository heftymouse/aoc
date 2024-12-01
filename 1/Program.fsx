open System.IO

let e = [for i in File.ReadAllLines("1/input.txt") do i.Split "   "]

let a, b =
    e
    |> List.fold (
        fun (l1, l2) b -> 
            ((b[0] |> int) :: l1, (b[1] |> int) :: l2)
        )
        ([], [])
        


let sortedA = List.sort a
let sortedB = List.sort b

let count = List.fold2 (fun acc x y -> acc + abs (x - y)) 0 sortedA sortedB

printfn $"{count}"

// -----------------

let bMap =
    sortedB 
    |> List.fold 
        (fun (acc: Map<int,int>) (i: int) -> 
            acc.Change(i,
                function
                    | Some x -> Some (x + 1)
                    | None -> None
                )
        )
        (Map([for i in sortedB do (i, 0)]))

let count2 =
    List.fold
        (fun acc x -> 
            acc + (x * (if bMap.ContainsKey(x) then bMap[x] else 0))
        )
        0
        sortedA 

printfn $"{count2}"