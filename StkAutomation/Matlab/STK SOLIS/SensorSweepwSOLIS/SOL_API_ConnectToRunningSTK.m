function STK = SOL_API_ConnectToRunningSTK()
    global STK 
    Paths.SOLIS_Exe    = winqueryreg('HKEY_LOCAL_MACHINE','SOFTWARE\AGI\Solis\v11','SolisExePath');
    Paths.SOLIS_Bin    = fileparts(Paths.SOLIS_Exe);

    STK.IntegrationLicense = true;
    if isfield(STK,'SOL_NetAssembly')
       STK.SOL_NetAssembly.delete;
       STK = rmfield(STK,'SOL_NetAssembly'); %#ok<*NASGU>
    end

    % Load the SOLIS DLL Assembly
    STK.SOL_NetAssembly = NET.addAssembly(fullfile(Paths.SOLIS_Bin,'STKSolisLib.dll'));

    % An STK Scenario must be open for the STKSolis.SolisConnect command to work
    halt = true;
    while halt
       try
          tic;
          disp('Attempting connection to SOLIS...');
          STK.SOL_Connect = STKSolis.SolisConnect;
          toc;
          disp('Connection successful.')
          disp('');
          halt = false;
       catch
          toc;
          disp('Connection failed.  May need to run the following windows command as an admin:');
          disp('netsh http add urlacl url="http://+:80/ASI/SolisConnect/Server" sddl=D:(A;;GX;;;S-1-1-0)')
       end
    end
end