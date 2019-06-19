$(document).ready(function () {
  if ($(window).width() <= 992) {
    $(".navbar-brand").click(function (e) {
      setTimeout(function () {
        if ($("#mobile-main-menu").hasClass("in")) {
          $("#mobile-main-menu").removeClass("in");
          $("#mobile-main-menu").attr("aria-expanded", false);
          let iconSrc = "/assets/images/icons/icon-hamburger.png";
          let imgIcon = $("#navbar-toggle").find("img");
          imgIcon[0].setAttribute("src", iconSrc);
        }
      }, 1000);
    });

    $("#navbar-toggle").click(function (e) {
      e.preventDefault();
      setTimeout(function () {
        let findImageMenu = $(this).find("img");
        if (findImageMenu && findImageMenu.length > 0) {
          let iconSrc = "";
          if ($(this).attr("aria-expanded") == "true") {
            iconSrc = "/assets/images/icons/icon-close-toggler-mobile.png";
            findImageMenu[0].setAttribute("src", iconSrc);
          } else {
            iconSrc = "/assets/images/icons/icon-hamburger.png";
            findImageMenu[0].setAttribute("src", iconSrc);
            // close filter menu if user toggle main menu
            if ($(".filter-menu--main").attr("aria-expanded") == "true") {
              $(".filter-menu--main").removeClass("in");
            }
            if ($(".filter-menu--step-one").attr("aria-expanded") == "true") {
              $(".filter-menu--step-one").removeClass("in");
            }
            if ($(".filter-menu--step-two").attr("aria-expanded") == "true") {
              $(".filter-menu--step-two").removeClass("in");
            }
            if ($(".filter-menu--step-three").attr("aria-expanded") == "true") {
              $(".filter-menu--step-three").removeClass("in");
            }
          }
        }
      }, 500);
    });
  }

  initialSlickSlider();

  initialClickEvent();

  $(window).scroll(function () {
    var header = $('.header'),
      scroll = $(window).scrollTop();

    if ($(window).width() > 992) {
      if (scroll > 100) header.addClass('sticky-header');
      else header.removeClass('sticky-header');
    } else {
      if (scroll > 0) header.addClass('sticky-header');
      else header.removeClass('sticky-header');
    }
  });

  $('#accordion').on('show.bs.collapse', '.collapse', function () {
    $('#accordion').find('.collapse.in').collapse('hide');
  });

  initialWishListItemsCount("feastWatson_WishList");

  document.addEventListener("wishListUpdated", function (e) {
    if (e.detail > 0) {
      $("#wish-list").text("Wish List (" + e.detail + ")");
      $(".icon-shopping-card").addClass("in-stock");
    } else {
      $("#wish-list").text("Wish List");
      $(".icon-shopping-card").removeClass("in-stock");
    }
  });

  if (localStorage.getItem('cookieAccepted') != 'true') {
    $(".cookie-banner").addClass("is-show");
  }

  $('.cmp-rich-text p').addClass("heading-text--contact");

  $('.subscribe___footer--error').css('display', 'none');

  const observer = lozad();
  observer.observe();
  handleSubmitFollowUsForm();

  $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
    $('.slider-nav').slick('setPosition');
    $('.slider-get-the-look').slick('setPosition');
    adjustItemCaptionHeight();
  });

  $('a[data-toggle="collapse"]').click(function (e) {
    $(".collapse").on('shown.bs.collapse', function (e) {
      $('.slider-nav').slick('setPosition');
      $('.slider-get-the-look').slick('setPosition');
    });
  });

  $(window).on('resize', function () {
    resizeCarouselIndicator();
    setTimeout(function () {
      generateSliderIndex(".slider-nav");
      generateSliderIndex(".slider-get-the-look");
      generateSliderIndex(".get-the-look__slider");
    }, 500);
  });

  adjustItemCaptionHeight();
});

function adjustItemCaptionHeight() {
  let maxHeight = 0;
  $(".responsive-caption").each(function () {
    if ($(this).height() > maxHeight) {
      maxHeight = $(this).height();
    }
  });

  if (maxHeight !== 0) {
    $(".responsive-caption").height(maxHeight);
  }
}

function handleSubmitFollowUsForm() {
  var $form = $("div[class*='follow-us-form']").find('form');
  var $emailField = $form.find('.emailAddress');
  var $btnSubmit = $form.find(':submit');
  var $validationMsg = $form.find('.subscribe___footer--error');
  var dataRegexPattern = $emailField.attr('data-val-regex-pattern');
  var emailErrorMsg = $emailField.attr('data-val-regex');
  var successMsg = "<span class='umbraco-forms-submitmessage'>" + $('#idSubscribeSuccessfulMessage').val() + "</span>";
  var alreadySubscribed = $('#idAlreadySubscribeMessage').val();

  $form.on('submit', function (e) {
    e.preventDefault();
    $validationMsg.css('display', 'none');

    if (!isEmail(dataRegexPattern, $emailField.val())) {
      $('.subscribe___footer--error').css('display', 'block');
      $validationMsg.text(emailErrorMsg);
      $emailField.css('margin-bottom', '0px');
    } else {
      var data = { 'email': $emailField.val() };
      $(".spinning").removeClass("hidden");
      $btnSubmit.prop('disabled', true);

      $.ajax({
        url: `${AppConfig.apis.isEmailSubscribed}`,
        type: 'GET',
        data: data,
        success: function (result) {
          setTimeout(function () {
            if (result) {
              $(".spinning").addClass("hidden");
              $btnSubmit.prop('disabled', false);
              $('.subscribe___footer--error').css('display', 'block');
              $emailField.css('margin-bottom', '0px');

              $validationMsg.text(alreadySubscribed);
            } else {
              $.ajax({
                url: $form.attr('action'),
                type: 'POST',
                cache: false,
                data: $form.serialize(),
                success: function (result) {
                  $('.subscribe___footer--error').css('display', 'block');
                  $form.html(successMsg);
                }
              });
            }
          }, 500);
        }
      });
    }
  });
}

function isEmail(regexPattern, email) {
  var regex = RegExp(regexPattern);
  return regex.test(email);
}

function initialWishListItemsCount(c_name) {
  let wishListItems = JSON.parse(localStorage.getItem(c_name));

  if (wishListItems && wishListItems.length > 0) {
    const countedItems = wishListItems.reduce((total, item) => total + item.quantity, 0);
    $("#wish-list").text("Wish List (" + countedItems + ")");
    $(".icon-shopping-card").addClass("in-stock");
  }
}

function getSelectedColor() {
  let colorBlock = document.getElementsByClassName('product-detail__colors-block text-center active');
  if (colorBlock.length > 0 && colorBlock[0].getElementsByTagName('img').length > 0) {
    return colorBlock[0].getElementsByTagName('img')[0].getAttribute('alt');
  }

  return '';
}

function initialClickEvent() {
  $('.btn-gallery-prev').click(function (event) {
    $('#' + event.currentTarget.dataset.targetUid + ' .gallery-component').slick('slickPrev');
  });

  $('.btn-gallery-next').click(function (event) {
    $('#' + event.currentTarget.dataset.targetUid + ' .gallery-component').slick('slickNext');
  });

  $("#close-btn").click(function () {
    setTimeout(function () {
      if ($("#nav-laptop").attr("aria-expanded") == "false") {
        $("#products-menu-desktop").removeClass("in");
        $("#product-view-menu-laptop").removeClass("in");
      }
    }, 500);
  });

  $(".navbar-search").click(function () {
    $(".header__search").toggleClass("show");
    $(".search-bar__input").focus();
  });

  $("#btn-close-search").click(function () {
    $(".header__search").toggleClass("show");
  });

  $(window).resize(function () {
    let windowHeight = $(window).height();
    $("#sidebar-cart").css({
      maxHeight: windowHeight
    });
  });

  $("#wish-list").click(function () {
    if ($("#sidebar-cart").attr("aria-hidden") === 'true') {

      let windowHeight = $(window).height();
      $("#sidebar-cart").css({
        maxHeight: windowHeight
      });

      $("#sidebar-cart").attr("aria-hidden", false);
      $(".page-overlay").addClass("is-visible");
      $('html, body').css({
        overflow: 'hidden'
      });
    }
  });

  $(".navbar-shopping-card").click(function (e) {
    let windowHeight = $(window).innerHeight();

    $("#sidebar-cart").css({
      maxHeight: windowHeight
    });

    $("#sidebar-cart").attr("aria-hidden", false);
    $(".page-overlay").addClass("is-visible");
    $('html, body').css({
      overflow: 'hidden',
      height: '100%'
    });

    $(window).resize(function () {
      let windowHeight = $(window).innerHeight();
      $("#sidebar-cart").css({
        maxHeight: windowHeight
      });
    });

    $(window).on('orientationchange', function () {
      $(window).one('resize', function () {
        let windowHeight = $(window).innerHeight();
        $("#sidebar-cart").css({
          maxHeight: windowHeight
        });
      });
    });

  });

  $("#close-wish-list").click(function () {
    $("#sidebar-cart").attr("aria-hidden", true);
    $(".page-overlay").removeClass("is-visible");
    $('html, body').css({
      overflow: 'auto'
    });
  });

  $(".scroll-btn").click(function (e) {
    const customId = $(this).attr('data-custom-id');
    if (document.getElementById(customId)) {
      const offSetHeight = document.getElementById(customId).offsetHeight;
      let offSetHeightToScroll = offSetHeight;
      if (window.pageYOffset >= offSetHeight) {
        offSetHeightToScroll = offSetHeightToScroll + window.pageYOffset;
      }

      event.preventDefault();
      $('html, body').animate({
        scrollTop: offSetHeightToScroll - 50
      }, 100);
    }
  });

  $("#btn-accept-cookie").click(function (e) {
    localStorage.setItem('cookieAccepted', 'true');
    $(".cookie-banner").removeClass("is-show");
  });

  $("#wishListBtn").click(function () {
    let wishListItems = JSON.parse(localStorage.getItem("feastWatson_WishList"));
    let selectedColor = getSelectedColor();
    let windowHeight = $(window).innerHeight();

    let newWishItemId = this.getAttribute('data-product-id') + '-' + selectedColor + '-' + $('#available-sizes').val();
    if (wishListItems) {
      const itemIndex = wishListItems.findIndex(item => item.id === newWishItemId);
      if (itemIndex >= 0) {
        wishListItems[itemIndex].quantity = wishListItems[itemIndex].quantity + 1;

        localStorage.setItem("feastWatson_WishList", JSON.stringify(wishListItems));
        document.dispatchEvent(new Event("wishListUpdated"));

        $("#sidebar-cart").css({
          maxHeight: windowHeight
        });

        $("#sidebar-cart").attr("aria-hidden", false);
        $(".page-overlay").addClass("is-visible");
        $('html, body').css({
          overflow: 'hidden',
          height: '100%'
        });

        $(window).resize(function () {
          let windowHeight = $(window).innerHeight();
          $("#sidebar-cart").css({
            maxHeight: windowHeight
          });
        });
  
        $(window).on('orientationchange', function () {
          $(window).one('resize', function () {
            let windowHeight = $(window).innerHeight();
            $("#sidebar-cart").css({
              maxHeight: windowHeight
            });
          });
        });

        countedItems = wishListItems.reduce((total, item) => total + item.quantity, 0);
        $("#wish-list").text("Wish List (" + countedItems + ")");
        $(".icon-shopping-card").addClass("in-stock");
        return;
      }
    }

    $("#sidebar-cart").attr("aria-hidden", false);
    $(".page-overlay").addClass("is-visible");
    $('html, body').css({
      overflow: 'hidden'
    });
    document.dispatchEvent(new CustomEvent("newWishItemAdded", { 'detail': newWishItemId }));

  });

  $(".page-overlay").click(function () {
    $("#sidebar-cart").attr("aria-hidden", true);
    $(".page-overlay").removeClass("is-visible");
    $('html, body').css({
      overflow: 'auto',
      height: 'auto'
    });
  });

  $("#btnContact").click(function () {
    setTimeout(function () {
      if ($('.field-validation-error').length === 0) {
        $('.button__text--contact').prop('hidden', true);
        $('.spinning--contact').removeClass("hidden");
      }
    }, 100);
  });

  $("#btnSubscribe").click(function () {
    setTimeout(function () {
      if ($('.field-validation-error').length === 0) {
        $('.button__text--subscribe').prop('hidden', true);
        $('.spinning--subscribe').removeClass("hidden");
      }
    }, 100);
  });
}

function initialSlickSlider() {
  $('.slider-nav').slick({
    slidesToShow: 3,
    slidesToScroll: 1,
    lazyLoad: 'ondemand',
    dots: true,
    arrows: true,
    centerMode: false,
    infinite: true,
    responsive: [
      {
        breakpoint: 1024,
        settings: {
          slidesToShow: 3,
          slidesToScroll: 3,
          infinite: true,
          dots: true
        }
      },
      {
        breakpoint: 768,
        settings: {
          slidesToShow: 2,
          slidesToScroll: 1,
          arrows: true,
          infinite: true,
          dots: true
        }
      },
      {
        breakpoint: 480,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
          arrows: true,
          infinite: true,
          dots: true
        }
      }
    ]
  });
  generateSliderIndex(".slider-nav");

  $('.slider-get-the-look').slick({
    slidesToShow: 3,
    slidesToScroll: 1,
    dots: true,
    lazyLoad: 'ondemand',
    arrows: true,
    centerMode: false,
    focusOnSelect: true,
    infinite: true,
    responsive: [
      {
        breakpoint: 1024,
        settings: {
          slidesToShow: 3,
          slidesToScroll: 3,
          infinite: true,
          dots: false
        }
      },
      {
        breakpoint: 768,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1
        }
      },
      {
        breakpoint: 480,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1
        }
      }
    ]
  });
  generateSliderIndex(".slider-get-the-look");

  $('.slider-nav-carousel').slick({
    dots: true,
    arrows: false,
    centerMode: false,
    lazyLoad: 'ondemand',
    pauseOnHover: false,
    pauseOnFocus: false,
    infinite: true,
    vertical: true,
    verticalSwiping: true,
    responsive: [
      {
        breakpoint: 480,
        settings: {
          verticalSwiping: false
        }
      },
      {
        breakpoint: 768,
        settings: {
          verticalSwiping: false
        }
      },
      {
        breakpoint: 1024,
        settings: {
          verticalSwiping: false
        }
      }
    ]
  });

  $('.gallery-component').slick({
    slidesToShow: 3,
    slidesToScroll: 1,
    dots: false,
    arrows: false,
    variableWidth: true,
    centerMode: true,
    infinite: true,
    speed: 1000,
    lazyLoad: 'progressive'
  });

  $('.get-the-look__slider').slick({
    slidesToShow: 1,
    slidesToScroll: 1,
    dots: true,
    lazyLoad: 'ondemand',
    arrows: true,
    centerMode: false,
    infinite: true,
    responsive: [
      {
        breakpoint: 1024,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
          dots: false
        }
      },
      {
        breakpoint: 768,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1
        }
      },
      {
        breakpoint: 480,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1
        }
      }
    ]
  });
  generateSliderIndex(".get-the-look__slider");

  $('.product-detail__slider').slick({
    slidesToShow: 1,
    slidesToScroll: 1,
    lazyLoad: 'progressive',
    dots: true,
    arrows: false,
    centerMode: false,
    focusOnSelect: true,
    infinite: true,
    responsive: [
      {
        breakpoint: 1024,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
          infinite: true,
          dots: false,
          arrows: true
        }
      },
      {
        breakpoint: 768,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
          infinite: true,
          dots: false,
          arrows: true
        }
      },
      {
        breakpoint: 480,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
          dots: true,
          arrows: false
        }
      }
    ]
  });
}

function jumpedSlider(sliderSelector) {
  let matchingIndicator = {};
  if (sliderSelector[0] !== undefined) {
    if (sliderSelector[0].className.includes("get-the-look__slider")) {
      matchingIndicator = $(sliderSelector[0]).siblings(".carousel-progress-bar");
    } else {
      matchingIndicator = $(sliderSelector[0]).parent().siblings().find(".carousel-progress-bar");
    }
    if (matchingIndicator.length > 0) {
      const indicatorId = "#" + matchingIndicator[0].id;
      let totalSlides = 0;
      const checkSlideDots = $("#" + sliderSelector[0].id + " .slick-dots");
      if (checkSlideDots.length > 0) {
        totalSlides = checkSlideDots[0].children.length;
      }

      let slickNext = $(sliderSelector[0]).find(".slick-next");
      if (slickNext.length > 0) {
        $(sliderSelector[0]).on('click', '.slick-next', function () {
          indicatorStepToNext(indicatorId, totalSlides);
        });
      }

      let slickPrev = $(sliderSelector[0]).find(".slick-prev");
      if (slickPrev.length > 0) {
        $(sliderSelector[0]).on('click', '.slick-prev', function () {
          indicatorStepToPrevious(indicatorId, totalSlides);
        });
      }

      $(sliderSelector[0]).on('swipe', function (event, slick, direction) {
        if (direction === 'left') {
          indicatorStepToNext(indicatorId, totalSlides);
        } else {
          indicatorStepToPrevious(indicatorId, totalSlides);
        }
      });
    }
  }
}

function guidGenerator() {
  var idGenerated = function () {
    return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
  };
  return (idGenerated() + idGenerated() + "-" + idGenerated() + "-" + idGenerated() + "-" + idGenerated() + "-" + idGenerated() + idGenerated() + idGenerated());
}

function generateSliderIndex(sliderSelector) {
  $(sliderSelector).each(function () {
    let slideId = guidGenerator();
    $(this).attr("id", slideId);
    let indicatorId = guidGenerator();
    let indicator = {};
    if (sliderSelector.includes("get-the-look__slider")) {
      indicator = $(this).siblings(".carousel-progress-bar");
    } else {
      indicator = $(this).parent().siblings().find(".carousel-progress-bar");
    }
    if (indicator.length > 0) {
      $(indicator[0]).attr("data-slide-id", slideId);
      $(indicator[0]).attr("id", indicatorId);
      jumpedSlider($("#" + slideId));
    }
  });
}

function indicatorStepToPrevious(indicatorId, totalSlides) {
  const parentWidth = $(indicatorId + "> .steps > div").width();
  let getIndicatorWidth = $(indicatorId + "> .steps > div > .indicator").width();
  let amountToJump = ((parentWidth - (getIndicatorWidth * totalSlides)) / (totalSlides - 1)) + getIndicatorWidth;

  let prevPosition = parseFloat($(indicatorId + " > .steps > div > .indicator").css("left"), 10);
  if (prevPosition > 0 && prevPosition > getIndicatorWidth) {
    prevPosition = prevPosition - amountToJump;
    $(indicatorId + "> .steps > div > .indicator").css("left", prevPosition + "px");
  } else {
    const endPoint = parentWidth - getIndicatorWidth;
    $(indicatorId + "> .steps > div > .indicator").css("left", endPoint);
  }
}

function indicatorStepToNext(indicatorId, totalSlides) {
  const parentWidth = $(indicatorId + "> .steps > div").width();
  let getIndicatorWidth = $(indicatorId + "> .steps > div > .indicator").width();
  let amountToJump = ((parentWidth - (getIndicatorWidth * totalSlides)) / (totalSlides - 1)) + getIndicatorWidth;

  const prevPosition = parseFloat($(indicatorId + " > .steps > div > .indicator").css("left"), 10);
  const nextPosition = prevPosition + amountToJump;
  if (nextPosition + getIndicatorWidth <= parentWidth) {
    $(indicatorId + "> .steps > div > .indicator").css("left", nextPosition + "px");
  } else {
    $(indicatorId + "> .steps > div > .indicator").css("left", 0);
  }
}

function resizeCarouselIndicator() {
  $(".carousel-progress-bar > .steps > div > .indicator").css("left", 0);
}