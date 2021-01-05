module Cockpit

open Color

type Cockpit =
    { StandardDeviation: StandardDeviation
      InventoryTarget: InventoryTarget
      InventoryAmount: InventoryAmount
      OrderPoint: OrderPoint }
and StandardDeviation = StandardDeviation of sd: uint
and InventoryTarget = InventoryTarget of inventoryTarget: uint
and InventoryAmount = NormalAmount of amount: uint | ExcessiveAmount of amount: uint
and OrderPoint = OrderPoint of orderPoint: uint

let colors =
    {| Red = Color.create (Red 255) (Green 0) (Blue 0)
       Yellow = Color.create (Red 255) (Green 255) (Blue 0)
       Green = Color.create (Red 0) (Green 255) (Blue 0)
       Black = Color.create (Red 0) (Green 0) (Blue 0)
       DarkGreen = Color.create (Red 40) (Green 167) (Blue 69)
       Mustard = Color.create (Red 255) (Green 193) (Blue 7)
       Blue = Color.create (Red 23) (Green 162) (Blue 1) |}

type Stack = Stack of stackName: string
type XAxisID = XAxisID of xAxisId: string

type Band =
    { Label: Label
      Height: Height
      BackgroundColor: Color }
and Label = Label of label: string
and Height = Height of height: uint

module SD =
    let toHeight (StandardDeviation sd) =
        Height sd

    let toBands (sd: StandardDeviation) =
        let sdHeight = toHeight sd
        let toBand color label = { Label = (Label label); Height = sdHeight; BackgroundColor = color }

        let redBand = toBand colors.Red
        let yellowBand = toBand colors.Yellow
        let greenBand = toBand colors.Green

        [
            redBand "-3 sd"
            yellowBand "-2 sd"
            greenBand "-1 sd"
            greenBand "1 sd"
            yellowBand "2 sd"
            redBand "3 sd"
        ]

    let toJS band =
        let (Label label, Height height) = (band.Label, band.Height)
        let color = band.BackgroundColor

        {| label = label
           data = [| height |]
           stack = "sd"
           xAxisID = "std-dev"
           backgroundColor = toRGBAstring color |}

module InventoryAmount =
    let toHeight inventoryAmount =
        match inventoryAmount with
        | NormalAmount amt -> Height amt
        | ExcessiveAmount amt -> Height amt

    let toColor inventoryAmount =
        match inventoryAmount with
        | NormalAmount _ -> colors.DarkGreen
        | ExcessiveAmount _ -> colors.Blue

    let toBand (inventoryAmount: InventoryAmount) =
        let height = toHeight inventoryAmount
        let color = toColor inventoryAmount

        { Label = Label "Inventory Amount"
          Height = height
          BackgroundColor = color }

    let toJS band =
        let (Label label, Height height) = (band.Label, band.Height)
        let color = band.BackgroundColor

        {| label = label
           data = [| height |]
           backgroundColor = toRGBAstring color
           xAxisID = "inventory" |}

module InventoryTargetLine =
    let toJS (InventoryTarget inventoryTarget) =
        {| label = "Inventory Target"
           ``type`` = "line"
           xAxisID = "std-dev"
           borderColor = "black"
           borderWidth = 2
           fill = false
           data = [| inventoryTarget; inventoryTarget |] |}

module OrderPointLine =
    let toJs (OrderPoint op) =
        {| label = "Order Point"
           ``type`` = "line"
           xAxisID = "std-dev"
           borderColor = toRGBAstring colors.Mustard
           borderWidth = 2
           fill = false
           data = [| op; op |] |}

module Chart =
    let sdAxis =
        {| id = "inventory"
           ``type`` = "category"
           categoryPercentage = 0.8
           barPercentage = 1.0
           display = true |}

    let inventoryAxis =
        {| id = "std-dev"
           ``type`` = "category"
           categoryPercentage = 0.2
           barPercentage = 1.0
           display = false |}

    let options (cockpit: Cockpit) =
        let (StandardDeviation sd) = cockpit.StandardDeviation

        {| legend = {| display = false |}
           scales =
               {| xAxes = [ sdAxis; inventoryAxis ]
                  yAxes = [{| ticks =
                              {| max = sd * 6u |} |}] |} |}