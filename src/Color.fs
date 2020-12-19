module Color

open System

type Red = Red of int
type Green = Green of int
type Blue = Blue of int

let clamp num = Math.Clamp(num, 0, 255)

type Color = { R: Red; G: Green; B: Blue }

let create (Red r) (Green g) (Blue b) =
    { R = Red(clamp r)
      G = Green(clamp g)
      B = Blue(clamp b) }

let toRGBAstring color =
    let { R = (Red r)
          G = (Green g)
          B = (Blue b) } =
        color

    $"rgba({r}, {g}, {b}, 1)"
