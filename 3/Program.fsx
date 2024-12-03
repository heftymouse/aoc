open System.IO
open System.Text.RegularExpressions

let data = File.ReadAllText("3/input.txt")

let mul = 
    Regex(@"mul\((\d+),(\d+)\)").Matches(data)
    |> Seq.fold (fun acc e -> acc + ((int e.Groups[1].Value) * (int e.Groups[2].Value))) 0

printfn "%d" mul

// -----------------

let (|StartsWith|_|) (p:string) (m:Match) =
    if m.Value.StartsWith p then
        if p.Equals("mul") then
            Some (List.map int (List.tail [ for x in m.Groups -> x.Value ]))
        else Some []
    else None

let mul2 = 
    Regex(@"(?:do\(\))|(?:don't\(\))|(?:mul\((\d+),(\d+)\))").Matches(data)
    |> Seq.fold
        (fun (acc:int, shouldDo:bool) e ->
            match e with
            | StartsWith "mul" e -> (if shouldDo then acc + e[0] * e[1] else acc), shouldDo
            | StartsWith "do(" _ -> acc, true
            | StartsWith "don't(" _ -> acc, false
            | _ -> acc, shouldDo
        )
        (0, true)

printfn $"{mul2}"