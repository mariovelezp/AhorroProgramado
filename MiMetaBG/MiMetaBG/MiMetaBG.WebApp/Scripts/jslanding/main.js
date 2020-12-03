$( document ).ready(function() {
  // Icons dropdown
  $('.dream-icon-select').each(function() {
    var buttonTab, listTab;

    buttonTab = $(this).find(".dream-icon-copy");

    listTab = $(this).find(".dream-icon-list");

    buttonTab.on("click", function() {
      listTab.slideToggle();
      return buttonTab.toggleClass("active");
    });

    listTab.children().children().on("click", function() {
      buttonTab.html($(this).html());
      listTab.slideToggle();
      return buttonTab.toggleClass("active");
    });

    $(document).on("click",function(e) {
      var container = $(".dream-icon-copy");
      if (!container.is(e.target) && container.has(e.target).length === 0) {
        $('.dream-icon-list').slideUp();
        $('.dream-icon-copy').removeClass('active');
      }
    });
  });

  // Dream detail menu
  $('.dream-detail-opt').on('click', function() {
    $(this).find('.dream-detail-menu').slideToggle();
  });

  $(document).on("click",function(e) {
    var container = $(".dream-detail-opt");
    if (!container.is(e.target) && container.has(e.target).length === 0) {
      $('.dream-detail-menu').slideUp();
    }
  });

  // Modals
  $('#noClientModal').modal('show');

  $('#noCelModal').modal('show');

  $('#noAccountModal').modal('show');

  // Carousel
  $(".carousel").on("touchstart", function(event){
    var xClick = event.originalEvent.touches[0].pageX;
    $(this).one("touchmove", function(event){
      var xMove = event.originalEvent.touches[0].pageX;
      if( Math.floor(xClick - xMove) > 5 ){
        $(this).carousel('next');
      }
      else if( Math.floor(xClick - xMove) < -5 ){
        $(this).carousel('prev');
      }
    });
    $(".carousel").on("touchend", function(){
      $(this).off("touchmove");
    });
  });

  // Carousel images
  $(document).on('classAdded', '#clientsSayCarousel .carousel-item:nth-child(1)', function(event, className) {
    if(className == "active") {
      $(".client-img-wrapper .client-img:nth-child(1)").addClass("active");
    } else {
      $(".client-img-wrapper .client-img:nth-child(1)").removeClass("active");
    }
  });

  $(document).on('classAdded', '#clientsSayCarousel .carousel-item:nth-child(2)', function(event, className) {
    if(className == "active") {
      $(".client-img-wrapper .client-img:nth-child(2)").addClass("active");
    } else {
      $(".client-img-wrapper .client-img:nth-child(2)").removeClass("active");
    }
  });

  $(document).on('classAdded', '#clientsSayCarousel .carousel-item:nth-child(3)', function(event, className) {
    if(className == "active") {
      $(".client-img-wrapper .client-img:nth-child(3)").addClass("active");
    } else {
      $(".client-img-wrapper .client-img:nth-child(3)").removeClass("active");
    }
  });

  $(document).on('classAdded', '#clientsSayCarousel .carousel-item:nth-child(4)', function(event, className) {
    if(className == "active") {
      $(".client-img-wrapper .client-img:nth-child(4)").addClass("active");
    } else {
      $(".client-img-wrapper .client-img:nth-child(4)").removeClass("active");
    }
  });

  $(document).on('classAdded', '#clientsSayCarousel .carousel-item:nth-child(5)', function(event, className) {
    if(className == "active") {
      $(".client-img-wrapper .client-img:nth-child(5)").addClass("active");
    } else {
      $(".client-img-wrapper .client-img:nth-child(5)").removeClass("active");
    }
  });

  $(document).on('classAdded', '#clientsSayCarousel .carousel-item:nth-child(6)', function(event, className) {
    if(className == "active") {
      $(".client-img-wrapper .client-img:nth-child(6)").addClass("active");
    } else {
      $(".client-img-wrapper .client-img:nth-child(6)").removeClass("active");
    }
  });
});

// Dreamm Progress Circle
$('.dream-detail-circle').each(function() {
  var progressData = $(this).find('.data-percent').text();

  var progressSubtract = 100 - progressData;

  $(this).find('.o-progress-circle__fill circle:nth-child(3)').attr('stroke-dashoffset', progressSubtract);
});

$('.data-percent').each(function () {
  $(this).prop('Counter',0).animate({
    Counter: $(this).text()
  }, {
    duration: 1000,
    easing: 'swing',
    step: function (now) {
      $(this).text(Math.ceil(now));
    }
  });
});

// Only Internet Explorer
var ua = window.navigator.userAgent;
var is_ie = /MSIE|Trident/.test(ua);

if ( is_ie ) {
  $('.o-progress-circle__fill circle:nth-child(3)').each(function () {
    $(this).prop('Counter',100).animate({
      Counter: $(this).attr('stroke-dashoffset')
    }, {
      duration: 1000,
      easing: 'swing',
      step: function (now) {
        $(this).attr('stroke-dashoffset', Math.ceil(now));
      }
    });
  });
}

// Class change detect
(function( func ) {
  $.fn.addClass = function(n) { // replace the existing function on $.fn
    this.each(function(i) { // for each element in the collection
      var $this = $(this); // 'this' is DOM element in this context
      var prevClasses = this.getAttribute('class'); // note its original classes
      var classNames = $.isFunction(n) ? n(i, prevClasses) : n.toString(); // retain function-type argument support
      $.each(classNames.split(/\s+/), function(index, className) { // allow for multiple classes being added
        if( !$this.hasClass(className) ) { // only when the class is not already present
          func.call( $this, className ); // invoke the original function to add the class
          $this.trigger('classAdded', className); // trigger a classAdded event
        }
      });
      prevClasses != this.getAttribute('class') && $this.trigger('classChanged'); // trigger the classChanged event
    });
    return this; // retain jQuery chainability
  }
})($.fn.addClass); // pass the original function as an argument

(function( func ) {
  $.fn.removeClass = function(n) {
    this.each(function(i) {
      var $this = $(this);
      var prevClasses = this.getAttribute('class');
      var classNames = $.isFunction(n) ? n(i, prevClasses) : n.toString();
      $.each(classNames.split(/\s+/), function(index, className) {
        if( $this.hasClass(className) ) {
          func.call( $this, className );
          $this.trigger('classRemoved', className);
        }
      });
      prevClasses != this.getAttribute('class') && $this.trigger('classChanged');
    });
    return this;
  }
})($.fn.removeClass);
