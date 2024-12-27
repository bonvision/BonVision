.\bonsai\modules\Export-Image.ps1 "..\src\BonVision\bin\Release\net462"
dotnet docfx metadata
python ./bonsai/template/api/plugins/Patch-IncludeWorkflow.py
dotnet docfx build
