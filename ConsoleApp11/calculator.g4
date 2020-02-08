grammar calculator;
/*
 * Parser Rules
 */
program:statement program | EOF;

statement:                assignment
                        | ifstatement
                        | EOL
                        ;

ifstatement          : 'if' '(' expr0 ')' EOL statement 'else' EOL statement ;

/*whilestatement       :  'while' '(' expr0 ')' EOL statement;
*/
assignment:IDENTIFIER OP_ASSIGN expr0 EOL;

expr0                :    expr1 #Exp1
                        | expr1 OP_EQU expr1 #OP_EQU
                        | expr1 OP_GT expr1 #OP_GT
                        | expr1 OP_GTEQU expr1 #OP_GTEQU
                        | expr1 OP_LESS expr1 #OP_LESS
                        | expr1 OP_LESSEQU expr1 #OP_LESSEQU
                        ;

expr1                :      expr1 ADD expr2 #Add    
                          | expr1 SUB expr2 #Subtraction
                          | expr2 #Exp2
                          ;
expr2                :      expr2 MUL expr3 #Mul   
                          | expr2 DIV expr3 #Div
                          | expr3 #Exp3
                          ;

expr3                :   NUMBER #Number
                      | IDENTIFIER #GetValueIDENTIFIER
                      | '(' expr1 ')'  #Expr1Parentheses;
/*
 * Lexer Rules
 */
NUMBER       : [0-9]+;
ADD          : '+' ;
SUB          : '-' ;
MUL          : '*' ;
DIV          : '/' ;
OP_ASSIGN    : '=';
OP_GT        : '>';
OP_GTEQU     : '>=';
OP_EQU       : '==';
OP_LESS      : '<';
OP_LESSEQU   : '<=';
IDENTIFIER:[a-zA-Z][a-zA-Z0-9]*;
WS : (' '|'\t') -> skip;
EOL : ('\r'?'\n'|'\r') ;
