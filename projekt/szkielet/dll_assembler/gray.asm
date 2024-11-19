.code



;takes 7 args
;1,2,3 - weights for grayscaling, in order BGR
;after setup: red in r10, green in r9, blue in r8
;4 - sum of weights in r11
;5 - pointer to byte array to modify in rsi
;6 - array size in rcx
;7 - number of bytes to increment the counter by (in rbx): 4 if alpha is present, 3 if not

GrayPixels proc


setup:
;sum of weights in r11
mov r11, r9
;red in r10, green in r9, blue in r8
mov r10, r8
mov r9, rdx
mov r8, rcx
;pop the array pointer to 
mov rsi, [rsp+40]
;pop the array size
mov rcx, [rsp+48]
;pop the array pointer increment value
mov rbx, [rsp+56]


turnOnePixelGray:

;reset the value for the sum register
mov r12, 0
;multiply each (weight) by (pixel color value)
;blue
mov rax, 0
mov al, [rsi]
mul r8
add r12, rax
;green
mov rax, 0
mov al, [rsi+1]
mul r9
add r12, rax
;red
mov rax, 0
mov al, [rsi+2]
mul r10
add r12, rax
;divide the sum of multiplications by the sum of weights
mov rax, r12
mov rdx, 0
div r11

;TESTS
;mov rax, 960
;mov rdx, 0
;mov r11, 18
;div r11
;TESTS

;overwrite the pixel color values in the array with the division result 
mov byte ptr [rsi], al
mov byte ptr [rsi+1], al
mov byte ptr [rsi+2], al
;increment the array pointer by 4 (to skip 3 colors and alpha)
add rsi, rbx
;decrement the array size register
sub rcx, 1
;jump to turnOnePixelGray unless the array size is zero (the procedure is then finnished)
cmp rcx, 0
jz quit
jmp turnOnePixelGray



quit:
mov rax, r12
ret


GrayPixels endp

end