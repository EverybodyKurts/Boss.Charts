module Cockpit

open Color

type Cockpit =
    { StandardDeviation: StandardDeviation
      InventoryTarget: InventoryTarget
      CurrentInventoryAmount: CurrentInventoryAmount
      OrderPoint: OrderPoint
      ExcessiveInventory: bool }
and StandardDeviation = StandardDeviation of int
and InventoryTarget = InventoryTarget of int
and CurrentInventoryAmount = CurrentInventoryAmount of int
and OrderPoint = OrderPoint of int

let colors =
    {| Red = Color.create (Red 255) (Green 0) (Blue 0)
       Yellow = Color.create (Red 255) (Green 255) (Blue 0)
       Green = Color.create (Red 0) (Green 255) (Blue 0)
       Black = Color.create (Red 0) (Green 0) (Blue 0)
       DarkGreen = Color.create (Red 40) (Green 167) (Blue 69)
       Mustard = Color.create (Red 255) (Green 193) (Blue 7)
       Blue = Color.create (Red 23) (Green 162) (Blue 1) |}

type Stack = Stack of string
type XAxisID = XAxisID of string

type Band =
    { Label: Label
      Height: Height
      BackgroundColor: Color }
and Label = Label of string
and Height = Height of float

module SD =
    let toBands (StandardDeviation sd) =
        let redBand label = { Label = (Label label); Height = (Height (float sd)); BackgroundColor = colors.Red }
        let yellowBand label = { Label = (Label label); Height = (Height (float sd)); BackgroundColor = colors.Yellow }
        let greenBand label = { Label = (Label label); Height = (Height (float sd)); BackgroundColor = colors.Green }

        [
            redBand "-3 sd"
            yellowBand "-2 sd"
            greenBand "-1 sd"
            greenBand "1 sd"
            yellowBand "2 sd"
            redBand "3 sd"
        ]

    let toJS band =
        let (Label label) = band.Label
        let (Height height) = band.Height
        let color = band.BackgroundColor

        {| label = label
           data = [| height |]
           stack = "sd"
           xAxisID = "std-dev"
           backgroundColor = toRGBAstring color |}

module Inventory =
    let toJS band =
        let (Label label, Height height) = (band.Label, band.Height)
        let color = band.BackgroundColor

        {| label = label
           data = [| height |]
           backgroundColor = toRGBAstring color
           xAxisID = "inventory" |}

module InventoryTargetLine =
    let toJS (InventoryTarget it) =
        {| label = "Inventory Target"
           ``type`` = "line"
           xAxisID = "std-dev"
           borderColor = "black"
           borderWidth = 2
           fill = false
           data = [| it; it |] |}

module OrderPointLine =
    let toJs (OrderPoint op) =
        {| label = "Order Point"
           ``type`` = "line"
           xAxisID = "std-dev"
           borderColor = toRGBAstring colors.Mustard
           borderWidth = 2
           fill = false
           data = [| op; op |] |}