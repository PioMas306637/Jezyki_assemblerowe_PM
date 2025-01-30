.code

; ====== GrayPixelsVector ======
;
; This procedure takes an array of bytes, representing color values of pixels.
;	For each three bytes (RGB), it calculates their weighted average and overwrites
;	all three with that average value. This effectively converts the image represented
;	by the array to grayscale. The procedure does not perform extensive validation
;	of input arguments, so it should be used with caution to avoid exceptions such as
;	"Attempted to read or write protected memory".
;
; This procedure takes 8 arguments
; 1 - the weight of blue, automaticaly moved to rcx
; 2 - the weight of red, automaticaly moved to rdx
; 3 - the weight of green, automaticaly moved to r8
; 4 - the sum of weights, automaticaly moved to r9
; 5 - the amount of pixels for the procedure to process,
;	taken from the stack, and stored in rcx for the duration of the procedure
; 6 - the amount of bytes used for describing a single pixel,
;	it is 4 if alpha channel is present, 3 if not,
;	taken from the stack, and stored in rbx for the duration of the procedure
; 7 - the starting offset for this procedure, used in multithreaded execution.
;	It is added to the array pointer and used as the pixel, from which the procedure
;	starts operating,
;	taken from the stack, and stored in r14 for the setup phase
;	(later, a loop counter is stored in r14)
; 8 - a pointer to the array of bytes to overwrite,
;	taken from the stack, and stored in rsi for the duration of the procedure
;
; Provided correct execution, this procedure returns a 0 in the RAX register.
;
; This procedure destroys the contents of the following registers during its operation:
;	- RAX
;	- RCX
;	- RDX
;	- R8
;	- R9
;	- YMM0
;	- YMM1
;	- YMM2
;	- YMM3
;	- YMM4
;	- YMM5
;	- YMM6
;	- YMM7
;
; This procedure uses but restores the following registers before it ends:
;	- RBX
;	- R14
;	- RSI

GrayPixelsVector proc

setup:
cvtsi2ss xmm1, r9					; sum converted from int to float for easier calculations
vbroadcastss ymm1, xmm1				; broadcast sum of weights to all 8 cells of ymm1
cvtsi2ss xmm2, rdx					; convert
vbroadcastss ymm2, xmm2				; broadcast red weight
cvtsi2ss xmm3, r8					; convert
vbroadcastss ymm3, xmm3				; broadcast green weight
cvtsi2ss xmm4, rcx					; convert
vbroadcastss ymm4, xmm4				; broadcast blue weight
push rbx							; save the contents of these registers to restore later on
push r14
push rsi
mov rcx, [rsp+64]					; pop the array size
mov rbx, [rsp+72]					; pop the array pointer increment value
mov r14, [rsp+80]					; pop the array starting index
mov rsi, [rsp+88]					; pop the array pointer 
add rsi, r14						; start turning pixels gray from the index indicated by r14

prepare_loop_counter:
; if rcx(remaining pixels) >= 4
;	r14(loop_counter) = 4
;	r9(loop_counter_non_modifiable) = 4
; if rcx < 4
;	r14 = rcx
;	r9 = rcx
cmp rcx, 4            
jge set_to_4
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


									; convert all floats in ymm0 back to integers and
									; send them back where they belong (overwrite source array)
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
pop rsi
pop r14
pop rbx
mov rax, 0
mov eax, ecx
ret




GrayPixelsVector endp




end