.code



;takes 7 args
;1,2,3 - weights for grayscaling, in order BGR
;4 - sum of weights
;wB in RCX, wG in RDX, wR in R8, Sw in R9
;5 - pointer to byte array to modify
;6 - array size

GrayPixels proc


setup:

;pop the array pointer to 
mov rsi, [rsp+40]
;pop the array size
mov r10, [rsp+48]


turnOnePixelGray:

;reset the value for the sum register
mov r12, 0
;multiply each (weight) by (pixel color value)
;blue
mov rax, [rsi]
mul rcx
add r12, rax
;green
mov rax, [rsi+1]
mul rdx
add r12, rax
;red
mov rax, [rsi+2]
mul r8
add r12, rax
;divide the sum of multiplications by the sum of weights
mov rax, r12
div r9
;overwrite the pixel color values in the array with the division result 
mov [rsi], rax
mov [rsi+1], rax
mov [rsi+2], rax
;increment the array pointer by 4 (to skip 3 colors and alpha)
add rsi, 4
;decrement the array size register
sub r10, 1
;jump to turnOnePixelGray unless the array size is zero (the procedure is then finnished)
cmp r10, 0
jz quit
jmp turnOnePixelGray



quit:
mov rax, r10
ret


GrayPixels endp

end