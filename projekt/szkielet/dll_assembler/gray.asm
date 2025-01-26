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



;takes 8 args
;1,2,3 - weights for grayscaling, in order BGR
;after setup: red in r10, green in r9, blue in r8
;4 - sum of weights in r11
;5 - pointer to byte array to modify in rsi
;6 - array size in rcx
;7 - number of bytes to increment the counter by (in rbx): 4 if alpha is present, 3 if not
;8 - starting array offset

GrayPixelsMulti proc

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

;pop the array starting index
mov r14, [rsp+64]
;start turning pixels gray from the index indicated by r14
add rsi, r14

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

GrayPixelsMulti endp



GrayPixelsVector proc


;					rcx   rdx   r8    r9
; GrayPixelsVector(0.2f, 0.3f, 0.5f, 1.0f, amountOfPixels, bytesForOnePixel, byteOffset, arrayForAss);

setup:
movd xmm1, r9d
vbroadcastss ymm1, xmm1		;sum broadcasted to all 8 cells of ymm1

movd xmm2, r8d
vbroadcastss ymm2, xmm2		;red
movd xmm3, edx
vbroadcastss ymm3, xmm3		;green
movd xmm4, ecx
vbroadcastss ymm4, xmm4		;blue weight in ymm4


mov ecx, [rsp+40]		; pop the array size
mov rbx, 0
mov ebx, [rsp+48]		; pop the array pointer increment value
mov r14d, [rsp+56]		; pop the array starting index
mov rsi, [rsp+64]		; pop the array pointer 

add rsi, r14			; start turning pixels gray from the index indicated by r14
mov r14, 8				; load loop counter (8) to r14




small_loop:
movzx rax, byte ptr [rsi]			; fetch first 8 bits (blue) to acumulator, fill remaining 64-8 bits with 0s
cvtsi2ss xmm7, rax					; convert integer in rax to a float in xmm7
movzx rax, byte ptr [rsi+1]			; fetch second 8 bits (green)
cvtsi2ss xmm6, rax					; convert
movzx rax, byte ptr [rsi+2]			; fetch third 8 bits (red)
cvtsi2ss xmm5, rax					; convert
add rsi, rbx						; increment array pointer
sub ecx, 1
jz quit								
sub r14, 1							; decrement loop counter
jz end_small_loop								
pslldq xmm7, 4						; shift xmm7 left by 4 bytes (float length) (to make room for the next converted value)
pslldq xmm6, 4						; shift xmm6 left by 4 bytes
pslldq xmm5, 4						; shift xmm5 left by 4 bytes
jmp small_loop						; do this 8 times, don't shift the 8th time

end_small_loop:
mov r14, 8							; restore the small_loop counter
									; multiply colours by their respective weights
									; ymm7 = ymm7*ymm4
									; ymm6 = ymm6*ymm3
									; ymm5 = ymm5*ymm2
vmulps ymm7, ymm7, ymm4
vmulps ymm6, ymm6, ymm3
vmulps ymm5, ymm5, ymm2
									; then sum all of them
									; ymm0 = ymm7 + ymm6 + ymm5
vaddps ymm0, ymm7, ymm6
vaddps ymm0, ymm0, ymm5
									; then divide by sum o weights
									; ymm0 = ymm0/ymm1
vdivps ymm0, ymm0, ymm1


									; convert all floats in ymm0 back to integers and put send them back where they belong (overwrite source array)
small_loop_2:
cvtss2si rax, xmm0					; convert rightmost float to integer in rax
sub rsi, rbx						; decrement array pointer
mov byte ptr [rsi], al				; overwrite
mov byte ptr [rsi+1], al
mov byte ptr [rsi+2], al
psrldq xmm0, 4						; shift the sum register right
sub r14, 1							; decrement loop counter
jz end_small_loop_2
jmp small_loop_2
end_small_loop_2:

mov r14, 8		
add rsi, rbx
add rsi, rbx
add rsi, rbx
add rsi, rbx
add rsi, rbx
add rsi, rbx
add rsi, rbx
add rsi, rbx

jmp small_loop


quit:
mov rax, 0
mov eax, ecx
ret







GrayPixelsVector endp




end