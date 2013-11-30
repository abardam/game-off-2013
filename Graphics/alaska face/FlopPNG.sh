#!/bin/bash
# Horizontally flips all png images in this folder using ImageMagick

for image in *.png 
do
    name="${image%.*}"
    imageflop="$name""flop.png"
    convert $image -flop $imageflop
done
