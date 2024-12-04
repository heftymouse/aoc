open System.IO
open System.Text.RegularExpressions

let data = File.ReadAllText("4/input.txt")

let hCount = Regex(@"XMAS").Matches(data).Count + Regex(@"SAMX").Matches(data).Count

// printfn "%d" hCount

let charArray = array2D ((data.Split '\n') |> Array.map (fun e -> e.ToCharArray()))

let vCount =
    [|0.. Array2D.length1 charArray - 1|]
    |> Array.fold
        (fun acc i ->
            let str = new string(charArray[*,i])
            acc + Regex(@"XMAS").Matches(str).Count + Regex(@"SAMX").Matches(str).Count
        )
        0

// printfn "%d" (vCount + hCount)

let mutable dCount = 0

do 
charArray 
|> Array2D.iteri
    (fun i j _ ->
        if ((i <= Array2D.length1 charArray - 4 && j <= Array2D.length2 charArray - 4) 
            && (
                (charArray[i,j] = 'X'
                && charArray[i+1,j+1] = 'M'
                && charArray[i+2,j+2] = 'A'
                && charArray[i+3,j+3] = 'S'
                ) || (charArray[i,j] = 'S'
                && charArray[i+1,j+1] = 'A'
                && charArray[i+2,j+2] = 'M'
                && charArray[i+3,j+3] = 'X')
            )
        ) 
        then dCount <- dCount + 1

        if((i <= Array2D.length1 charArray - 4 && j >= 3)
            && (
                (charArray[i,j] = 'X'
                && charArray[i+1,j-1] = 'M'
                && charArray[i+2,j-2] = 'A'
                && charArray[i+3,j-3] = 'S'
                ) || (charArray[i,j] = 'S'
                && charArray[i+1,j-1] = 'A'
                && charArray[i+2,j-2] = 'M'
                && charArray[i+3,j-3] = 'X')
            )
        )   
        then dCount <- dCount + 1
    )

printfn "%d" (hCount + vCount + dCount)

// -----------------

let mutable xCount = 0

do 
charArray 
|> Array2D.iteri
    (fun i j _ ->
        if (
            (i <= Array2D.length1 charArray - 3 && j <= Array2D.length2 charArray - 3) 
            && charArray[i+1,j+1] = 'A'
            && (
                (
                    (charArray[i,j] = 'M'
                    && charArray[i+2,j+2] = 'S')
                    || (charArray[i,j] = 'S'
                    && charArray[i+2,j+2] = 'M')
                ) && (
                    (charArray[i,j+2] = 'M'
                    && charArray[i+2,j] = 'S')
                    || (charArray[i,j+2] = 'S'
                    && charArray[i+2,j] = 'M')
                )
            )
        ) 
        then xCount <- xCount + 1
    )

printfn "%d" xCount