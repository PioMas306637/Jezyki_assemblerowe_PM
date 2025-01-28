.code



GrayPixelsVector proc


;					blue  red  green   sum
;					rcx   rdx   r8    r9      rsp+40             rsp+48			rsp+56      rsp+64
; GrayPixelsVector(int, int, int, int, amountOfPixels, bytesForOnePixel, byteOffset, arrayForAss);

setup:
cvtsi2ss xmm1, r9				; sum converted from int to float for easier calculations
vbroadcastss ymm1, xmm1			; broadcast sum of weights to all 8 cells of ymm1
cvtsi2ss xmm2, rdx				; convert
vbroadcastss ymm2, xmm2			; broadcast red weight
cvtsi2ss xmm3, r8				; convert
vbroadcastss ymm3, xmm3			; broadcast green weight
cvtsi2ss xmm4, rcx				; convert
vbroadcastss ymm4, xmm4			; broadcast blue weight


mov rcx, [rsp+40]		; pop the array size
mov rbx, [rsp+48]		; pop the array pointer increment value
mov r14, [rsp+56]		; pop the array starting index
mov rsi, [rsp+64]		; pop the array pointer 

add rsi, r14			; start turning pixels gray from the index indicated by r14
;mov r14, 4				; load loop counter (4) to r14


prepare_loop_counter:
; if rcx(remaining pixels) >= 4
; r14(loop_counter) = 4
; r9(loop_counter_non_modifiable) = 4
; if rcx < 4
; r14 = rcx
; r9 = rcx

cmp rcx, 4            
jge set_to_4			; jump to set_to_4 if rcx >= 4, otherwise just continue

cmp rcx, 0
je quit

mov r14, rcx          
mov r9, rcx          
jmp done               

set_to_4:
mov r14, 4
mov r9, 4

done:
sub rcx, r9



loop_read_from_array:
movzx rax, byte ptr [rsi]			; fetch first 8 bits (blue) to acumulator, fill remaining 64-8 bits with 0s
cvtsi2ss xmm7, rax					; convert to float and load blue value to xmm7
movzx rax, byte ptr [rsi+1]			; fetch second 8 bits (green)
cvtsi2ss xmm6, rax
movzx rax, byte ptr [rsi+2]			; fetch third 8 bits (red)
cvtsi2ss xmm5, rax
add rsi, rbx						; increment array pointer
sub r14, 1							; decrement loop counter
jz end_loop_read_from_array								
pslldq xmm7, 4						; shift xmm7 left by 4 bytes (float length) (to make room for the next converted value)
pslldq xmm6, 4						; shift xmm6 left by 4 bytes
pslldq xmm5, 4						; shift xmm5 left by 4 bytes
jmp loop_read_from_array			; do this 4times, don't shift the 4th time

end_loop_read_from_array:
mov r14, r9							; restore the loop counter
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
loop_write_to_array:
cvtss2si rax, xmm0					; convert rightmost float to integer in rax
sub rsi, rbx						; decrement array pointer
mov byte ptr [rsi], al				; overwrite
mov byte ptr [rsi+1], al
mov byte ptr [rsi+2], al
psrldq xmm0, 4						; shift the sum register right
sub r14, 1							; decrement loop counter
jz end_loop_write_to_array
jmp loop_write_to_array

end_loop_write_to_array:
mov r14, r9					; restore the small loop counter

loop_restore_rsi:
add rsi, rbx
sub r14, 1
jz end_loop_restore_rsi
jmp loop_restore_rsi

end_loop_restore_rsi:
mov r14, r9					; restore the small loop counter
jmp prepare_loop_counter


quit:
mov rax, 0
mov eax, ecx
ret




GrayPixelsVector endp




end