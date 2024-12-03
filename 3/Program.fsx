open System.IO
open System.Text.RegularExpressions

let data = File.ReadAllText("3/input.txt")

let mul = 
    Regex(@"mul\((\d+),(\d+)\)").Matches(data)
    |> Seq.fold (fun acc e -> acc + ((int e.Groups[1].Value) * (int e.Groups[2].Value))) 0

printfn "%d" mul

// -----------------

type Action =
    Mul of (int * int)
    | Do
    | Dont
    | Nop

let (|MatchVerb|_|) (m:Match) =
    match m.Groups["verb"].Value with
    | "mul" ->  Some (Mul (int m.Groups["arg1"].Value, int m.Groups["arg2"].Value))
    | "don't" -> Some Dont
    | "do" -> Some Do
    | _ -> None

let mul2 = 
    Regex(@"(?<verb>(?:don't)|(?:do)|(?:mul))\(((?<arg1>\d+),(?<arg2>\d+))*\)").Matches(data)
    |> Seq.fold
        (fun (acc:int, shouldDo:bool) e ->
            match e with
            | MatchVerb (Mul (x, y)) -> (if shouldDo then acc + (x * y) else acc), shouldDo
            | MatchVerb Do -> acc, true
            | MatchVerb Dont -> acc, false
            | _ -> acc, shouldDo
        )
        (0, true)

printfn $"{fst mul2}"