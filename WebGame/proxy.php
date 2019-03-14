<?php
	header('Content-Type: text/html; charset=utf-8');
	
	function convert($str)
	{
		return iconv('cp1251', 'utf-8', $str);
	}
	
	echo file_get_contents(str_replace("[and]", "&", str_replace("[plus]", "+", convert($_GET['r']))));
?>