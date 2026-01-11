using System;
using System.Collections.Generic;
using System.Text;

public class DialogueParser
{
    public List<DialogueActionContext> ActionParser(string actionsStr)
    {
        List<DialogueActionContext> actions = new();
        if (string.IsNullOrEmpty(actionsStr)) return actions;

        StringBuilder sb = new();
        bool inQuote = false;
        bool addingParams = false;
        
        DialogueActionContext currentAction = new();
        for (int i = 0; i < actionsStr.Length; i++)
        {
            char currentChar = actionsStr[i];
            
            if (currentChar == '\\')
            {
                if (i + 1 >= actionsStr.Length)
                    throw new Exception("Invalid escape at end of string");

                char c =  actionsStr[++i];
                sb.Append(c switch
                {
                    'n' => '\n',
                    'r' => '\r',
                    't' => '\t',
                    '"' => '"',
                    '\\' => '\\',
                    _ => c
                });

                continue;
            }
            
            if (currentChar == '"')
            {
                inQuote = !inQuote;
                continue;
            }

            if (!inQuote)
            {
                if (currentChar == '(')
                {
                    // 인자 리스트에 추가하기를 시작하기.
                    addingParams = true;

                    var funcName = sb.ToString().Trim();
                    if (string.IsNullOrEmpty(funcName))
                        throw new Exception("Invalid function name");
                        
                    currentAction.FunctionName = funcName;
                    sb.Clear();
                    
                    continue;
                }
                else if (currentChar == ',')
                {
                    // 인자 리스트에 추가하기.
                    if (!addingParams)
                        throw new Exception("')' without '('");
                    
                    currentAction.Parameters.Add(sb.ToString().Trim());
                    sb.Clear();
                    continue;
                }
                else if (currentChar == ')')
                {
                    if (!addingParams)
                        throw new Exception("')' without '('");
                    
                    // 인자 리스트에 추가하기를 마무리하기.
                    addingParams = false;
                    
                    string lastParam = sb.ToString().Trim();
                    sb.Clear();
                    if (lastParam.Length > 0) currentAction.Parameters.Add(lastParam);
                    continue;
                }
                else if (currentChar == ';')
                {
                    // 현재 작업중인 DialogueActionContext를 마무리하기.   
                    if (string.IsNullOrEmpty(currentAction.FunctionName))
                        throw new Exception("Invalid function name");
                    
                    actions.Add(currentAction);
                    currentAction = new();
                    sb.Clear();
                    inQuote = false;
                    addingParams = false;
                    continue;
                }
            }
            
            sb.Append(currentChar);
        }
        
        // Finalize 하기
        if (inQuote) throw new Exception("Unterminated quote");
        if (addingParams) throw new Exception("Unterminated parameters");
        
        if (!string.IsNullOrEmpty(currentAction.FunctionName))
            actions.Add(currentAction);
        
        return actions;
    }
}
