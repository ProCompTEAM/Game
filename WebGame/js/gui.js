var str_color_format = function(str)
{
	if(str.length < 3) return str;
	var color = [];
	
	color['f'] = 'white';
	color['0'] = 'black';
	color['1'] = 'navy';
	color['2'] = 'green';
	color['3'] = 'turquoise';
	color['4'] = 'red';
	color['5'] = 'purple';
	color['6'] = 'gold';
	color['7'] = 'grey';
	color['8'] = 'gray';
	color['9'] = 'blue';
	color['a'] = 'lime';
	color['b'] = 'cyan';
	color['c'] = 'tomato';
	color['d'] = 'magenta';
	color['e'] = 'yellow';
	
	var result = "";
	trg = false;
	
	for(i = 0; i < str.length; i++)
	{
		if(str[i] == '<') result += '</b>';
		
		if(str[i] == '§')
		{
			if(trg) result += '</b>';
			
			result += '<b style="color: ' + color[str[i + 1]] + ';">';
			
			trg = true;
			
			i++;
		}
		else result += str[i];
	}
	
	if(trg) result += '</b>';
	
	return result;
}

String.prototype.replaceAll = function(search, replacement) {
    var target = this;
    return target.split(search).join(replacement);
};

//0 - 5 - button
var activate_settings_item = function(iid)
{
	console.log("Activated settings item as " + iid);
	
	net_request_settings(PLAYER_TOKEN, iid);
}

//Form API

const _REPLACE = '~';

var status_form_ui_on = function()
{
	document.getElementById("ui_form").style.display = "block";
}

var status_form_ui_off = function()
{
	document.getElementById("ui_form").style.display = "none";
}

var ACTIVATED_FORM_DATA = []; //data for request about modified form

var CURRENT_FORM_KEY = "";

var create_form_ui = function(raw)
{
	ACTIVATED_FORM_DATA = [];
	
	data = raw.split(';');
	
	CURRENT_FORM_KEY = data[0];
	var title = data[1];
	var textcolor = data[2];
	var bgcolor = data[3];
	
	document.getElementById("ui_form_title").innerHTML = title;
	document.getElementById("ui_form_content").innerHTML = "";
	
	if(bgcolor != "") document.getElementById("ui_form_content").style.backgroundColor = bgcolor;
	if(textcolor != "") document.getElementById("ui_form_content").style.color = textcolor;
	
	for(let i = 4; i < data.length; i++)
	{	
		var props = data[i].split(',');
		var el = "";
		
		switch(Number.parseInt(props[1])) //control type
		{
			//Button : 0x01
			case 1:
				el += '<button onclick="activate_form_control(1, this); activate_form_ui();" item="' + props[0] + '">' + props[2].replaceAll(_REPLACE, ',') + '</button>';
			break;
			//ListGroup : 0x02
			case 2:
				el += '<select onchange="activate_form_control(2, this)" item="' + props[0] + '">';
					for(let j = 2; j < props.length; j++)
					{
						el += '<option>' + props[j].replaceAll(_REPLACE, ',') + '</option>';
					}
				el += '</select>';
			break;
			//TextBox : 0x03
			case 3:
				el += '<input onchange="activate_form_control(3, this)" type="text" value="' + props[2].replaceAll(_REPLACE, ',') + '" item="' + props[0] + '">';
			break;
			//ContentText : 0x04
			case 4:
				el += '<span item="' + props[0] + '">' + props[2].replaceAll(_REPLACE, ',') + '</span>';
			break;
			//PictureBox : 0x05
			case 5:
				el += '<img alt="picbox" src="' + props[2].replaceAll(_REPLACE, ',') + '" item="' + props[0] + '">';
			break;
		}
		
		document.getElementById("ui_form_content").innerHTML += el;
	}
	
	status_form_ui_on();
}

var activate_form_ui = function()
{
	var compressed = compress_activated_form_data();
	
	net_request_form(PLAYER_TOKEN, compressed);
	
	status_form_ui_off();
}

// 0x01 - Button
// 0x02 - ListGroup
// 0x03 - TextBox
var activate_form_control = function(typeItem, el)
{
	var id = el.getAttribute("item");
	
	switch(typeItem)
	{
		case 1:
			ACTIVATED_FORM_DATA[id] = el.innerText;
		break;
		
		case 2:
			ACTIVATED_FORM_DATA[id] = el.options.selectedIndex;
		break;
		
		case 3:
			ACTIVATED_FORM_DATA[id] = el.value;
		break;
	}
}

var compress_activated_form_data = function()
{
	//result : <FToken>;<control : <iId>,<value>>;...;<control : <iId>,<value>>
	var result = CURRENT_FORM_KEY + ";";
	
	for (itemId in ACTIVATED_FORM_DATA) 
	{
		result += itemId + "," + ACTIVATED_FORM_DATA[itemId] + ";";
	}
	
	return result.substr(0, result.length - 1);
}
