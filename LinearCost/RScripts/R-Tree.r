# requirements
require(grDevices)
require(dplyr)

# functions
drawSingleRectangle <- function(df, rectangleName) {
  Rectan <-df[df$Rectangle == rectangleName,]
  rect(min(Rectan$XCoord, na.rm = TRUE), min(Rectan$YCoord, na.rm = TRUE), max(Rectan$XCoord, na.rm = TRUE), max(Rectan$YCoord, na.rm = TRUE),)
  center <- c(mean(c(min(Rectan$XCoord, na.rm = TRUE), max(Rectan$XCoord, na.rm = TRUE))), mean(c(min(Rectan$YCoord, na.rm = TRUE), max(Rectan$YCoord, na.rm = TRUE))))
  text(center[1], center[2], labels = Rectan[1, 1])
  text(Rectan[Rectan$Point == "P1",]$XCoord, Rectan[Rectan$Point == "P1",]$YCoord-0.5, paste("x:", Rectan[Rectan$Point == "P1",]$XCoord, ", y:", Rectan[Rectan$Point == "P1",]$YCoord), cex = 0.75)
  text(Rectan[Rectan$Point == "P2",]$XCoord, Rectan[Rectan$Point == "P2",]$YCoord-0.5, paste("x:", Rectan[Rectan$Point == "P2",]$XCoord, ", y:", Rectan[Rectan$Point == "P2",]$YCoord), cex = 0.75)
  text(Rectan[Rectan$Point == "P3",]$XCoord, Rectan[Rectan$Point == "P3",]$YCoord+0.5, paste("x:", Rectan[Rectan$Point == "P3",]$XCoord, ", y:", Rectan[Rectan$Point == "P3",]$YCoord), cex = 0.75)
  text(Rectan[Rectan$Point == "P4",]$XCoord, Rectan[Rectan$Point == "P4",]$YCoord+0.5, paste("x:", Rectan[Rectan$Point == "P4",]$XCoord, ", y:", Rectan[Rectan$Point == "P4",]$YCoord), cex = 0.75)
}


drawRectangle <- function(df, rectangleNames, newRectangleName) {
  Rectans <- subset(df, Rectangle %in% rectangleNames)
  rect(min(Rectans$XCoord, na.rm = TRUE), min(Rectans$YCoord, na.rm = TRUE), max(Rectans$XCoord, na.rm = TRUE), max(Rectans$YCoord, na.rm = TRUE), border = "red", density = 10)
  center <- c(mean(c(min(Rectans$XCoord, na.rm = TRUE), max(Rectans$XCoord, na.rm = TRUE))), mean(c(min(Rectans$YCoord, na.rm = TRUE), max(Rectans$YCoord, na.rm = TRUE))))
  text(center[1], center[2]-2, labels = newRectangleName)
}


calculateArea <- function(df, RectangleNames) {
  Rectans <- subset(df, Rectangle %in% RectangleNames)
  xmin <- min(Rectans$XCoord)
  xmax <- max(Rectans$XCoord)
  ymin <- min(Rectans$YCoord)
  ymax <- max(Rectans$YCoord)
  return ((xmax - xmin) * (ymax - ymin))
}


calculateAreaDiff <- function(df2, RectangleOne, RectangleTwo) {
  return (df2[df2$RectangleNames == paste(RectangleOne, RectangleTwo), ]$Areas - df2[df2$RectangleNames == RectangleOne, ]$Areas - df2[df2$RectangleNames == RectangleTwo, ]$Areas)
}


startPlot <- function() {
  plot(c(1, max(df$XCoord)+1), c(1, max(df$YCoord)+1), type = "n", xlab = "x", ylab = "y", axis=FALSE)
  
  drawSingleRectangle(df, "R1")
  drawSingleRectangle(df, "R2")
  drawSingleRectangle(df, "R3")
  drawSingleRectangle(df, "R4")
  drawSingleRectangle(df, "R5")  
}


# Define Rectangles

Rectangle = c ("R1", "R1", "R1", "R1", "R2", "R2", "R2", "R2", "R3", "R3", "R3", "R3", "R4", "R4", "R4", "R4", "R5", "R5", "R5", "R5", "R1 R2", "R1 R2", "R1 R2", "R1 R2", "R4 R5", "R4 R5", "R4 R5", "R4 R5", "R1 R2 R3", "R1 R2 R3", "R1 R2 R3", "R1 R2 R3")
Point = c("P1", "P2", "P3", "P4", "P1", "P2", "P3", "P4", "P1", "P2", "P3", "P4", "P1", "P2", "P3", "P4", "P1", "P2", "P3", "P4", "P1", "P2", "P3", "P4", "P1", "P2", "P3", "P4", "P1", "P2", "P3", "P4")
XCoord = c(3, 6, 6, 3, 2, 5, 5, 2, 8, 10, 8, 10, 14, 18, 18, 14, 12, 20, 20, 12, 2, 6, 6, 2, 12, 20, 20, 12, 2, 10, 10, 2)
YCoord = c(2, 2, 4, 4, 7, 7, 11, 11, 6, 6, 9, 9, 9, 9, 14, 14, 2, 2, 8, 8, 2, 2, 11, 11, 2, 2, 14, 14, 2, 2, 11, 11)
df = data.frame(Rectangle, Point, XCoord, YCoord)

RectangleNames <- c(
  "R1",
  "R2",
  "R3",
  "R4",
  "R5",
  "R1 R2",
  "R1 R3",
  "R1 R4",
  "R1 R5",
  "R2 R3",
  "R2 R4",
  "R2 R5",
  "R3 R4",
  "R3 R5",
  "R4 R5",
  "R1 R2 R3",
  "R1 R2 R5",
  "R4 R5 R3"
)

Areas <- c(
  calculateArea(df, "R1"),
  calculateArea(df, "R2"),
  calculateArea(df, "R3"),
  calculateArea(df, "R4"),
  calculateArea(df, "R5"),
  calculateArea(df, c("R1", "R2")),
  calculateArea(df, c("R1", "R3")),
  calculateArea(df, c("R1", "R4")),
  calculateArea(df, c("R1", "R5")),
  calculateArea(df, c("R2", "R3")),
  calculateArea(df, c("R2", "R4")),
  calculateArea(df, c("R2", "R5")),
  calculateArea(df, c("R3", "R4")),
  calculateArea(df, c("R3", "R5")),
  calculateArea(df, c("R4", "R5")),
  calculateArea(df, c("R1", "R2", "R3")),
  calculateArea(df, c("R1", "R2", "R5")),
  calculateArea(df, c("R4", "R5", "R3"))
)

dfTmp <- data.frame(RectangleNames, Areas)

AreaDiff <- c(
  0,
  0,
  0,
  0,
  0,
  calculateAreaDiff(dfTmp, "R1", "R2"),
  calculateAreaDiff(dfTmp, "R1", "R3"),
  calculateAreaDiff(dfTmp, "R1", "R4"),
  calculateAreaDiff(dfTmp, "R1", "R5"),
  calculateAreaDiff(dfTmp, "R2", "R3"),
  calculateAreaDiff(dfTmp, "R2", "R4"),
  calculateAreaDiff(dfTmp, "R2", "R5"),
  calculateAreaDiff(dfTmp, "R3", "R4"),
  calculateAreaDiff(dfTmp, "R3", "R5"),
  calculateAreaDiff(dfTmp, "R4", "R5"),
  calculateAreaDiff(dfTmp, "R1 R2", "R3"),
  calculateAreaDiff(dfTmp, "R1 R2", "R5"),
  calculateAreaDiff(dfTmp, "R4 R5", "R3")
)

df2 <- data.frame(RectangleNames, Areas, AreaDiff)
# Draw

startPlot()
drawRectangle(df, "R1", "SR1")
drawRectangle(df, "R4", "SR2")

calculateAreaDiff(df2, "R1", "R2")
calculateAreaDiff(df2, "R1", "R3")
calculateAreaDiff(df2, "R1", "R5")
calculateAreaDiff(df2, "R2", "R4")
calculateAreaDiff(df2, "R3", "R4")
calculateAreaDiff(df2, "R4", "R5")

startPlot()
drawRectangle(df, "R1 R2", "SR1")
drawRectangle(df, "R4", "SR2")

calculateAreaDiff(df2, "R1 R2", "R3")
calculateAreaDiff(df2, "R1 R2", "R5")
calculateAreaDiff(df2, "R3", "R4")
calculateAreaDiff(df2, "R4", "R5")

startPlot()
drawRectangle(df, "R1 R2", "SR1")
drawRectangle(df, "R4 R5", "SR2")

calculateAreaDiff(df2, "R1 R2", "R3")
calculateAreaDiff(df2, "R4 R5", "R3")

startPlot()
drawRectangle(df, "R1 R2 R3", "SR1")
drawRectangle(df, "R4 R5", "SR2")
