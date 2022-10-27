function varargout = SSA_Clutter_Interference_Visualizer(varargin)
% SSA_CLUTTER_INTERFERENCE_VISUALIZER MATLAB code for SSA_Clutter_Interference_Visualizer.fig
%      SSA_CLUTTER_INTERFERENCE_VISUALIZER, by itself, creates a new SSA_CLUTTER_INTERFERENCE_VISUALIZER or raises the existing
%      singleton*.
%
%      H = SSA_CLUTTER_INTERFERENCE_VISUALIZER returns the handle to a new SSA_CLUTTER_INTERFERENCE_VISUALIZER or the handle to
%      the existing singleton*.
%
%      SSA_CLUTTER_INTERFERENCE_VISUALIZER('CALLBACK',hObject,eventData,handles,...) calls the local
%      function named CALLBACK in SSA_CLUTTER_INTERFERENCE_VISUALIZER.M with the given input arguments.
%
%      SSA_CLUTTER_INTERFERENCE_VISUALIZER('Property','Value',...) creates a new SSA_CLUTTER_INTERFERENCE_VISUALIZER or raises the
%      existing singleton*.  Starting from the left, property value pairs are
%      applied to the GUI before SSA_Clutter_Interference_Visualizer_OpeningFcn gets called.  An
%      unrecognized property name or invalid value makes property application
%      stop.  All inputs are passed to SSA_Clutter_Interference_Visualizer_OpeningFcn via varargin.
%
%      *See GUI Options on GUIDE's Tools menu.  Choose "GUI allows only one
%      instance to run (singleton)".
%
% See also: GUIDE, GUIDATA, GUIHANDLES

% Edit the above text to modify the response to help SSA_Clutter_Interference_Visualizer

% Last Modified by GUIDE v2.5 07-Dec-2019 11:40:50

% Begin initialization code - DO NOT EDIT
gui_Singleton = 1;
gui_State = struct('gui_Name',       mfilename, ...
                   'gui_Singleton',  gui_Singleton, ...
                   'gui_OpeningFcn', @SSA_Clutter_Interference_Visualizer_OpeningFcn, ...
                   'gui_OutputFcn',  @SSA_Clutter_Interference_Visualizer_OutputFcn, ...
                   'gui_LayoutFcn',  [] , ...
                   'gui_Callback',   []);
if nargin && ischar(varargin{1})
    gui_State.gui_Callback = str2func(varargin{1});
end

if nargout
    [varargout{1:nargout}] = gui_mainfcn(gui_State, varargin{:});
else
    gui_mainfcn(gui_State, varargin{:});
end
% End initialization code - DO NOT EDIT


% --- Executes just before SSA_Clutter_Interference_Visualizer is made visible.
function SSA_Clutter_Interference_Visualizer_OpeningFcn(hObject, eventdata, handles, varargin)
% This function has no output args, see OutputFcn.
% hObject    handle to figure
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
% varargin   command line arguments to SSA_Clutter_Interference_Visualizer (see VARARGIN)

% Choose default command line output for SSA_Clutter_Interference_Visualizer
handles.output = hObject;

% Update handles structure
guidata(hObject, handles);

% UIWAIT makes SSA_Clutter_Interference_Visualizer wait for user response (see UIRESUME)
% uiwait(handles.figSSAClutterInterferenceVis);


% --- Outputs from this function are returned to the command line.
function varargout = SSA_Clutter_Interference_Visualizer_OutputFcn(hObject, eventdata, handles) 
% varargout  cell array for returning output args (see VARARGOUT);
% hObject    handle to figure
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Get default command line output from handles structure
varargout{1} = handles.output;


% --- Executes on selection change in cboPlotDisplay.
function cboPlotDisplay_Callback(hObject, eventdata, handles)
% hObject    handle to cboPlotDisplay (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: contents = cellstr(get(hObject,'String')) returns cboPlotDisplay contents as cell array
%        contents{get(hObject,'Value')} returns selected item from cboPlotDisplay

update_display(handles) ;


% --- Executes during object creation, after setting all properties.
function cboPlotDisplay_CreateFcn(hObject, eventdata, handles)
% hObject    handle to cboPlotDisplay (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: popupmenu controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function txtImageSizeX_Callback(hObject, eventdata, handles)
% hObject    handle to txtImageSizeX (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtImageSizeX as text
%        str2double(get(hObject,'String')) returns contents of txtImageSizeX as a double


% --- Executes during object creation, after setting all properties.
function txtImageSizeX_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtImageSizeX (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function txtImageSizeY_Callback(hObject, eventdata, handles)
% hObject    handle to txtImageSizeY (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtImageSizeY as text
%        str2double(get(hObject,'String')) returns contents of txtImageSizeY as a double


% --- Executes during object creation, after setting all properties.
function txtImageSizeY_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtImageSizeY (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function txtNoise_Callback(hObject, eventdata, handles)
% hObject    handle to txtNoise (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtNoise as text
%        str2double(get(hObject,'String')) returns contents of txtNoise as a double


% --- Executes during object creation, after setting all properties.
function txtNoise_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtNoise (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function txtStarBrightness_Callback(hObject, eventdata, handles)
% hObject    handle to txtStarBrightness (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtStarBrightness as text
%        str2double(get(hObject,'String')) returns contents of txtStarBrightness as a double


% --- Executes during object creation, after setting all properties.
function txtStarBrightness_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtStarBrightness (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function txtIntegrationTime_Callback(hObject, eventdata, handles)
% hObject    handle to txtIntegrationTime (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtIntegrationTime as text
%        str2double(get(hObject,'String')) returns contents of txtIntegrationTime as a double


% --- Executes during object creation, after setting all properties.
function txtIntegrationTime_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtIntegrationTime (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function txtSystemPSFWidth_Callback(hObject, eventdata, handles)
% hObject    handle to txtSystemPSFWidth (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtSystemPSFWidth as text
%        str2double(get(hObject,'String')) returns contents of txtSystemPSFWidth as a double


% --- Executes during object creation, after setting all properties.
function txtSystemPSFWidth_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtSystemPSFWidth (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function txtStarDensity_Callback(hObject, eventdata, handles)
% hObject    handle to txtStarDensity (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtStarDensity as text
%        str2double(get(hObject,'String')) returns contents of txtStarDensity as a double


% --- Executes during object creation, after setting all properties.
function txtStarDensity_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtStarDensity (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function txtTargetDirX_Callback(hObject, eventdata, handles)
% hObject    handle to txtTargetDirX (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtTargetDirX as text
%        str2double(get(hObject,'String')) returns contents of txtTargetDirX as a double


% --- Executes during object creation, after setting all properties.
function txtTargetDirX_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtTargetDirX (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function txtTargetDirY_Callback(hObject, eventdata, handles)
% hObject    handle to txtTargetDirY (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtTargetDirY as text
%        str2double(get(hObject,'String')) returns contents of txtTargetDirY as a double


% --- Executes during object creation, after setting all properties.
function txtTargetDirY_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtTargetDirY (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function txtTargetBrightness_Callback(hObject, eventdata, handles)
% hObject    handle to txtTargetBrightness (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtTargetBrightness as text
%        str2double(get(hObject,'String')) returns contents of txtTargetBrightness as a double


% --- Executes during object creation, after setting all properties.
function txtTargetBrightness_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtTargetBrightness (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function txtTargetSpeed_Callback(hObject, eventdata, handles)
% hObject    handle to txtTargetSpeed (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtTargetSpeed as text
%        str2double(get(hObject,'String')) returns contents of txtTargetSpeed as a double


% --- Executes during object creation, after setting all properties.
function txtTargetSpeed_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtTargetSpeed (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function txtClutterDirX_Callback(hObject, eventdata, handles)
% hObject    handle to txtClutterDirX (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtClutterDirX as text
%        str2double(get(hObject,'String')) returns contents of txtClutterDirX as a double


% --- Executes during object creation, after setting all properties.
function txtClutterDirX_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtClutterDirX (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function txtClutterDirY_Callback(hObject, eventdata, handles)
% hObject    handle to txtClutterDirY (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtClutterDirY as text
%        str2double(get(hObject,'String')) returns contents of txtClutterDirY as a double


% --- Executes during object creation, after setting all properties.
function txtClutterDirY_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtClutterDirY (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function txtClutterSpacing_Callback(hObject, eventdata, handles)
% hObject    handle to txtClutterSpacing (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtClutterSpacing as text
%        str2double(get(hObject,'String')) returns contents of txtClutterSpacing as a double


% --- Executes during object creation, after setting all properties.
function txtClutterSpacing_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtClutterSpacing (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function txtClutterCount_Callback(hObject, eventdata, handles)
% hObject    handle to txtClutterCount (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtClutterCount as text
%        str2double(get(hObject,'String')) returns contents of txtClutterCount as a double


% --- Executes during object creation, after setting all properties.
function txtClutterCount_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtClutterCount (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function txtClutterAlongVar_Callback(hObject, eventdata, handles)
% hObject    handle to txtClutterAlongVar (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtClutterAlongVar as text
%        str2double(get(hObject,'String')) returns contents of txtClutterAlongVar as a double


% --- Executes during object creation, after setting all properties.
function txtClutterAlongVar_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtClutterAlongVar (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function txtClutterAcrossVar_Callback(hObject, eventdata, handles)
% hObject    handle to txtClutterAcrossVar (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtClutterAcrossVar as text
%        str2double(get(hObject,'String')) returns contents of txtClutterAcrossVar as a double


% --- Executes during object creation, after setting all properties.
function txtClutterAcrossVar_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtClutterAcrossVar (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function txtClutterOriginX_Callback(hObject, eventdata, handles)
% hObject    handle to txtClutterOriginX (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtClutterOriginX as text
%        str2double(get(hObject,'String')) returns contents of txtClutterOriginX as a double


% --- Executes during object creation, after setting all properties.
function txtClutterOriginX_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtClutterOriginX (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function txtClutterOriginY_Callback(hObject, eventdata, handles)
% hObject    handle to txtClutterOriginY (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtClutterOriginY as text
%        str2double(get(hObject,'String')) returns contents of txtClutterOriginY as a double


% --- Executes during object creation, after setting all properties.
function txtClutterOriginY_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtClutterOriginY (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function txtClutterBrightness_Callback(hObject, eventdata, handles)
% hObject    handle to txtClutterBrightness (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtClutterBrightness as text
%        str2double(get(hObject,'String')) returns contents of txtClutterBrightness as a double


% --- Executes during object creation, after setting all properties.
function txtClutterBrightness_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtClutterBrightness (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function txtClutterSpeed_Callback(hObject, eventdata, handles)
% hObject    handle to txtClutterSpeed (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtClutterSpeed as text
%        str2double(get(hObject,'String')) returns contents of txtClutterSpeed as a double


% --- Executes during object creation, after setting all properties.
function txtClutterSpeed_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtClutterSpeed (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackoldoldgroundColor'))
    set(hObject,'BackgroundColor','white');
end


% --- Executes on button press in cmdUpdate.
function cmdUpdate_Callback(hObject, eventdata, handles)
    % hObject    handle to cmdUpdate (see GCBO)
    % eventdata  reserved - to be defined in a future version of MATLAB
    % handles    structure with handles and user data (see GUIDATA)

    tic
    
    [clutter_information] = get_clutter_information(handles) ;

    % Generate clutter positions
    clutter_satellite_positions = create_satellite_train_layer(clutter_information.clutter_count,clutter_information.clutter_direction,clutter_information.clutter_spacing,clutter_information.clutter_along_var,clutter_information.clutter_across_var,0) ;
    clutter_satellite_positions(:,1) = clutter_satellite_positions(:,1) + clutter_information.clutter_origin(1) ;
    clutter_satellite_positions(:,2) = clutter_satellite_positions(:,2) + clutter_information.clutter_origin(2) ;
    
    handles.clutter_satellite_positions = clutter_satellite_positions ;
    
    % Generate target information
    target_information = get_target_information(handles) ;
    handles.target_information = target_information ;
    
    % Generate general image information
    handles.image_information = get_image_information(handles) ;
    sz = handles.image_information.sz ;
    integration_time = handles.image_information.integration_time ;
    
    % Generate star background information
    star_information = get_star_information(handles) ;
    [star_positions, star_brightness_levels] = create_star_info(sz, star_information.star_density, star_information.star_max_brightness) ;
    handles.star_positions = star_positions ;
    handles.star_brightness_levels = star_brightness_levels ;
    
    % Generate system PSF
    psf_std_dev = str2num(get(handles.txtSystemPSFWidth,'String')) ;
    system_psf = generate_2d_gaussian_psf(psf_std_dev,[-3*psf_std_dev:3*psf_std_dev],[-3*psf_std_dev:3*psf_std_dev]) ;
    
    % Generate target image layer
    target_start_pos = round(handles.target_information.target_origin) ;
    target_stop_pos = target_start_pos+target_information.target_direction*target_information.target_speed*integration_time ;
    
    if (target_start_pos(1) >= 1 && target_start_pos(2) >= 1 && target_start_pos(1) <= sz(2) && target_start_pos(2) <= sz(1))
        %target_map(target_start_pos(2),target_start_pos(1)) = handles.target_information.target_brightness*integration_time ;
        
        x_range = [1,sz(2)] ;
        y_range = [1,sz(1)] ;
        target_map = draw_antialias_line(target_start_pos(1),target_stop_pos(1),target_start_pos(2),target_stop_pos(2),x_range,y_range)*target_information.target_brightness*integration_time ;
    else
        target_map = zeros(sz) ;
    end
    
    handles.target_map = conv2(target_map,system_psf,'same') ;
    
    % Generate clutter image layer
    clutter_motion_vector = clutter_information.clutter_direction*integration_time*clutter_information.clutter_speed ;
    handles.clutter_map = conv2(create_clutter_map(sz,clutter_satellite_positions,clutter_information.clutter_brightness*integration_time, clutter_motion_vector),system_psf,'same') ;
    
    % Generate star image layer
    handles.star_map = conv2(create_star_map(sz,star_positions,star_brightness_levels*integration_time),system_psf,'same') ;
    
    % Generate noise map
    handles.noise_map = randn(sz)*handles.image_information.noise_level ;
    
    toc
    
    % Save the GUI data
    guidata(hObject, handles);
    
    tic
    update_display(handles) ;
    toc
    
function [clutter_information] = get_clutter_information(handles)
    clutter_information.clutter_spacing = str2num(get(handles.txtClutterSpacing,'String')) ;
    clutter_information.clutter_count = str2num(get(handles.txtClutterCount,'String')) ;
    clutter_information.clutter_brightness = str2num(get(handles.txtClutterBrightness,'String')) ;
    clutter_information.clutter_along_var = str2num(get(handles.txtClutterAlongVar,'String')) ;
    clutter_information.clutter_across_var = str2num(get(handles.txtClutterAcrossVar,'String')) ;
    clutter_information.clutter_origin = [str2num(get(handles.txtClutterOriginX,'String')), str2num(get(handles.txtClutterOriginY,'String'))] ;
    clutter_information.clutter_direction = [str2num(get(handles.txtClutterDirX,'String')), str2num(get(handles.txtClutterDirY,'String'))] ;
    % Normalize the direction vector
    direction_magnitude = clutter_information.clutter_direction*clutter_information.clutter_direction' ;
    if direction_magnitude ~= 1
        clutter_information.clutter_direction = clutter_information.clutter_direction/sqrt(direction_magnitude) ;
    end
    clutter_information.clutter_speed = str2num(get(handles.txtClutterSpeed,'String')) ;

function [target_information] = get_target_information(handles)
    target_information.target_origin = [str2num(get(handles.txtTargetOriginX,'String')), str2num(get(handles.txtTargetOriginY,'String'))] ;
    target_information.target_direction = [str2num(get(handles.txtTargetDirX,'String')), str2num(get(handles.txtTargetDirY,'String'))] ;
    % Normalize the direction vector
    direction_magnitude = target_information.target_direction*target_information.target_direction' ;
    if direction_magnitude ~= 1
        target_information.target_direction = target_information.target_direction/sqrt(direction_magnitude) ;
    end
    target_information.target_brightness = str2num(get(handles.txtTargetBrightness,'String')) ;
    target_information.target_speed = str2num(get(handles.txtTargetSpeed,'String')) ;
    

function [image_information] = get_image_information(handles)
    image_information.sz = [str2num(get(handles.txtImageSizeY,'String')), str2num(get(handles.txtImageSizeX,'String'))] ;
    image_information.noise_level = str2num(get(handles.txtNoise,'String')) ;
    image_information.integration_time = str2num(get(handles.txtIntegrationTime,'String')) ;
    
function [star_information] = get_star_information(handles)
    star_information.star_density = 1/str2num(get(handles.txtStarDensity,'String')) ;
    star_information.star_max_brightness = str2num(get(handles.txtStarBrightness,'String')) ;

function [] = update_display(handles)
    image_display_selection = get(handles.cboImageDisplay,'Value') ;
    
    if image_display_selection == 1
        % None
    elseif image_display_selection == 2
        % Target
        imagesc(handles.target_map+handles.noise_map) ;
    elseif image_display_selection == 3
        % Clutter
        imagesc(handles.clutter_map) ;
	elseif image_display_selection == 4
        % Stars
        imagesc(handles.star_map+handles.noise_map) ;
    elseif image_display_selection == 5
        % Target and clutter
        imagesc(handles.target_map+handles.clutter_map+handles.noise_map) ;
    elseif image_display_selection == 6
        % Target and stars
        imagesc(handles.target_map+handles.star_map+handles.noise_map) ;
    else
        % Target, clutter, and stars
        imagesc(handles.target_map+handles.clutter_map+handles.star_map+handles.noise_map) ;
    end
    
    colormap gray ;

    plot_display_selection = get(handles.cboPlotDisplay,'Value') ;
    
    if plot_display_selection == 1
        % No plotting at all
        
        if image_display_selection == 1
            imagesc(zeros(128,128)) ;
        end
        
        axis tight equal ;
    else
        if image_display_selection ~= 1
            hold on ;
        end
        
        if plot_display_selection == 2
            % Just target position
            plot(handles.target_information.target_origin(1),handles.target_information.target_origin(2),'go') ;
            axis tight equal ;
        elseif plot_display_selection == 3
            % Just clutter positions
            plot(handles.clutter_satellite_positions(:,1),handles.clutter_satellite_positions(:,2),'r.') ;
            axis tight equal ;
        elseif plot_display_selection == 4
            % Just star positions
            plot(handles.star_positions(:,1),handles.star_positions(:,2),'b*') ;
        elseif plot_display_selection == 5
            % Target and clutter
            plot(handles.target_information.target_origin(1),handles.target_information.target_origin(2),'go') ;
            hold on ;
            plot(handles.clutter_satellite_positions(:,1),handles.clutter_satellite_positions(:,2),'r.') ;
        elseif plot_display_selection == 6
            % Target and stars
            plot(handles.target_information.target_origin(1),handles.target_information.target_origin(2),'go') ;
            hold on ;
            plot(handles.star_positions(:,1),handles.star_positions(:,2),'b*') ;
        else
            % Target, clutter, and stars
            plot(handles.target_information.target_origin(1),handles.target_information.target_origin(2),'go') ;
            hold on ;
            plot(handles.clutter_satellite_positions(:,1),handles.clutter_satellite_positions(:,2),'r.') ;
            plot(handles.star_positions(:,1),handles.star_positions(:,2),'b*') ;
        end
        
        hold off ;
        axis tight equal ;
    end
    
    mouse_figure(handles.figSSAClutterInterferenceVis) ;



function txtTargetOriginX_Callback(hObject, eventdata, handles)
% hObject    handle to txtTargetOriginX (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtTargetOriginX as text
%        str2double(get(hObject,'String')) returns contents of txtTargetOriginX as a double


% --- Executes during object creation, after setting all properties.
function txtTargetOriginX_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtTargetOriginX (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function txtTargetOriginY_Callback(hObject, eventdata, handles)
% hObject    handle to txtTargetOriginY (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtTargetOriginY as text
%        str2double(get(hObject,'String')) returns contents of txtTargetOriginY as a double


% --- Executes during object creation, after setting all properties.
function txtTargetOriginY_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtTargetOriginY (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end


% --- Executes on selection change in cboImageDisplay.
function cboImageDisplay_Callback(hObject, eventdata, handles)
% hObject    handle to cboImageDisplay (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: contents = cellstr(get(hObject,'String')) returns cboImageDisplay contents as cell array
%        contents{get(hObject,'Value')} returns selected item from cboImageDisplay

update_display(handles) ;

% --- Executes during object creation, after setting all properties.
function cboImageDisplay_CreateFcn(hObject, eventdata, handles)
% hObject    handle to cboImageDisplay (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: popupmenu controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end


% --- Executes on button press in cmdSave.
function cmdSave_Callback(hObject, eventdata, handles)
% hObject    handle to cmdSave (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Save the image, use a dialog to get the image name
[file,path] = uiputfile('*.jpg','Image File');
if file ~= 0
    F = getframe(handles.axes1);
    Image = frame2im(F);
    imwrite(Image, [path file]);
end