open System
open System.IO

let data =
    File.ReadAllLines("6/input.txt")
    |> Array.map (fun x -> x.ToCharArray())
    |> array2D

let mutable (x, y) = (0, 0)

type Direction =
    Up
    | Down
    | Left
    | Right

do
data
    |> Array2D.iteri
        (fun i j e ->
            if e = '^' then do (
                x <- j
                y <- i
            )
        )

printfn "%d %d" x y

let nextDirection dir =
    match dir with
    | Up -> Right
    | Right -> Down
    | Down -> Left
    | Left -> Up

// let prevDirection dir =
//     match dir with
//     | Right -> Up
//     | Up -> Left
//     | Left -> Down
//     | Down -> Right

let nextCell x y dir =
    match dir with
    | Up -> x, y - 1
    | Down -> x, y + 1
    | Left -> x - 1, y
    | Right -> x + 1, y

do printfn "!!! %d %d" (Array2D.length1 data) (Array2D.length2 data)

let rec tracePath (marked:Set<int * int>) (x, y) dir =
    let newX, newY = nextCell x y dir

    if 0 > newX || newX >= Array2D.length1 data || 0 > newY || newY >= Array2D.length2 data
    then marked.Count
    else
    match data[newY, newX] with
    | '#' ->  tracePath marked (x, y) (nextDirection dir)
    | '.' | '^' -> tracePath (Set.add (newX, newY) marked) (newX, newY) dir
    | _ -> -1

printfn "%d" (tracePath (Set.ofList [(x, y)]) (x, y) Up)

// -----------------
